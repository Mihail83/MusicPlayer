using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skins;
using System.IO;
using System.Xml.Serialization;

namespace MusicPlayer
{
    public class VideoPlayer : GenericPlayer<Video>, IDisposable
    {
        public override void Load(string pathToFolder)
        {
            throw new NotImplementedException();
        }

        public override void Play()
        {
            throw new NotImplementedException();
        }
    }


    public class Player : GenericPlayer<Song>, IDisposable
    {        
       // private int counter;        
        
        Random rnd = new Random();

        public Song PlayingSong { get; private set; }

        private bool disposed = false;

        public Player()
        {
            //counter = 0;
            skin = new ClassicSkin();
            _playingItem = new List<Song>();
        }

        ///<summary>/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// AL6-Player -AudioFiles.
        /// Имя
        /// Дата создания
        /// Размер 
        /// </summary>
        public override void Load(string pathToFolder)   
        {
            DirectoryInfo directoryWithWav = new DirectoryInfo(pathToFolder);
            var wavInfo = directoryWithWav.GetFiles("*.wav");

            if (wavInfo != null)
            {
                foreach (var info in wavInfo)
                {
                    _playingItem.Add(new Song(info));
                }
            }
        }

        public override void Play()
        {
            if (!_play && _playingItem.Count > 0)
            {
                _play = true;
            }

            if (_play)
            {
                foreach (var song in _playingItem)
                {
                    PlayingSong = song;

                    using (System.Media.SoundPlayer player = new System.Media.SoundPlayer())
                    {
                        player.SoundLocation = PlayingSong.path;                        
                        skin.Render($"Player is playing: ");
                        skin.Render(song);
                        player.PlaySync();
                    }
                }
            }

            _play = false;
        }

       

        public void LockButton()
        {
            if (_locked)
            {
                _locked = false;
                Console.WriteLine("Плеер разблокирован/n");
            }
            else
            {
                _locked = true;
                Console.WriteLine("Плеер заблокирован\n");
            }
        }           

        public void FilterByGenre(Artist.Genre genre)
        {
            for (int i = _playingItem.Count - 1; i >= 0; i--)
            {
                if (_playingItem[i].Artist.genre != genre)
                {
                    _playingItem.RemoveAt(i);
                }
            }
        }

        public void SortByGenre()     
        {
            var sortedsongsLINQ = from n in _playingItem orderby n.Artist.genre select n;
            var listForSort = new List<Song>();                              // разобраться с преобразованием типов
            listForSort.AddRange(sortedsongsLINQ);
            _playingItem = listForSort;
        }

        ~Player()
        {
            Dispose(false);
        }

        //public void Dispose()   
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}



        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                
                
                // Free any other managed objects here.                
            }
            // Free any unmanaged objects here.

            disposed = true;
            base.Dispose(disposing);
        }


    }

      
    public abstract class GenericPlayer<T> : IDisposable where T : PlayingItem
    {
        protected bool _locked;
        protected bool _play;
        protected int _volume;
        public List<T> _playingItem;

        const int MIN_VOLUME = 0;
        const int MAX_VOLUME = 100;      

        protected ISkin skin;

        private bool disposed = false;

        public int Volume
        {
            get
            {
                return _volume;
            }

            protected set
            {
                if (value < MIN_VOLUME)
                {
                    _volume = MIN_VOLUME;
                }
                else if (value > MAX_VOLUME)
                {
                    _volume = MAX_VOLUME;
                }
                else
                {
                    _volume = value;
                }
            }
        }

        public bool Playing
        {
            get
            {
                return _play;
            }
        }

        public void VolumeUp()
        {
            if (!_locked)
            {
                Volume++;
                skin.Render("sound has been increased\n");
            }
        }

        public void VolumeDown()
        {
            if (!_locked)
            {
                Volume--;
                skin.Render("sound has been reduced\n");
            }
        }

        public void VolumeChange(int step)
        {
            if (!_locked)
            {

                Volume += step;
                if (step > 0)
                {
                    skin.Render("sound has been increased\n");
                }
                else
                {
                    skin.Render("sound has been reduced\n");
                }
            }
        }
                             
        public void SaveAsPlaylist(string name)
        {
            var songToXml = new XmlSerializer(typeof(List<T>));
            using (var xmlStream = new StreamWriter(@"D:\миша_документы\курсы 2018\С# basic\Wav\" + name + ".xml "))
            {
                songToXml.Serialize(xmlStream, _playingItem);
            }
        }

        public void LoadPlaylist(string path)
        {
            var songToXml = new XmlSerializer(typeof(List<T>));
            using (var xmlStream = new StreamReader(path))
            {
                _playingItem = (List<T>)songToXml.Deserialize(xmlStream);
            }
        }

        public abstract void Load(string pathToFolder);
       

        public void Clear()
        {
            _playingItem.Clear();
        }

        public void Remove(int RemoveIndex)
        {
            _playingItem.RemoveAt(RemoveIndex);
        }

        public void LazyAndRightSort()
        {
            _playingItem.Sort();
        }

        public void NewScreen()
        {
            skin.Clear();
        }

        public abstract void Play();

        public void Stop()
        {
            if (!_locked)
            {
                skin.Render("Player has stopped\n");
                _play = false;
            }
        }

        public void SortByTitle()
        {
            var listForSort = new List<T>();
            var linqSort = from song in _playingItem orderby song.Name select song;
            listForSort.AddRange(linqSort);            
            _playingItem = listForSort;            
        }
        public void Shuffle()
        {
            _playingItem.Shuffle();
        }

        public void Like(int number)
        {
            _playingItem[number].FieldLike = true;
        }

        public void Dislike(int number)
        {
            _playingItem[number].FieldLike = false;
        }

        ~ GenericPlayer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _playingItem = null;
                skin = null;
                // Free any other managed objects here.                
            }
            // Free any other unmanaged objects here.

            disposed = true;
        }
    }
}
