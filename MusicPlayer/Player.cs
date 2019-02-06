using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// using Skins;
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
        

        public Song PlayingSong { get; private set; }

        private bool disposed = false;
        

        public Player()
        {            
            _playingItem = new List<Song>();           
        }
        
        public event Action<List<Song>, Song, bool, int> SongsListChangedEvent;
        public event Action<List<Song>, Song, bool, int> SongStartedEvent;
        
        public event Action<List<Song>, Song, bool, int> PlayerStoppedEvent;
        //public event Action<List<Song>, Song, bool, int> PlayerStartedEvent;

        public event Action<List<Song>, Song, bool, int> PlayerUnLocked;
        public event Action<List<Song>, Song, bool, int> PlayerLocked;

        public override void Load(string source)
        {
            var dirInfo = new DirectoryInfo(source);

            if (dirInfo.Exists)
            {
                var files = dirInfo.GetFiles("*.wav");
                foreach (var file in files)
                {
                    _playingItem.Add(new Song(file));
                }
            }
            SongsListChangedEvent?.Invoke(_playingItem, null, _locked, _volume);
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
                    SongStartedEvent?.Invoke(_playingItem, song, _locked, _volume);

                    using (System.Media.SoundPlayer player = new System.Media.SoundPlayer())
                    {
                        player.SoundLocation = PlayingSong.path;                        
                        player.PlaySync();
                    }
                    PlayingSong = null; //снять выделение красным
                }
            }

            _play = false;
        }

        public override void Stop()
        {
            if (!_locked)
            {
                _play = false;
                PlayerStoppedEvent?.Invoke(_playingItem, null, _locked, _volume);
            }
        }

        public void LockButton()
        {
            if (_locked)
            {
                _locked = false;
                PlayerUnLocked?.Invoke(_playingItem, PlayingSong, _locked, _volume);
            }
            else
            {
                _locked = true;
                PlayerLocked?.Invoke(_playingItem, PlayingSong, _locked, _volume);
            }
        }                 

        public void SortByGenre()     
        {
            var sortedsongsLINQ = from n in _playingItem orderby n.Artist.genre select n;
            var listForSort = new List<Song>();                              // разобраться с преобразованием типов
            listForSort.AddRange(sortedsongsLINQ);
            _playingItem = listForSort;
            SongsListChangedEvent?.Invoke(_playingItem, PlayingSong, _locked, _volume);
        }

        ~Player()
        {
            Dispose(false);
        }

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
        protected bool _locked=false;
        protected bool _play;
        protected int _volume;
        public List<T> _playingItem;

        const int MIN_VOLUME = 0;
        const int MAX_VOLUME = 100;

        
        public event Action<List<T>, T, bool, int> VolumeChangedEvent;
        

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
                VolumeChangedEvent?.Invoke(_playingItem, null, _locked, _volume);
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
            }
        }

        public void VolumeDown()
        {
            if (!_locked)
            {
                Volume--;               
            }
        }

        public void VolumeChange(int step)
        {
            if (!_locked)
            {
                Volume += step;                
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

        public abstract void Play();

        public virtual void Stop()
        {
            if (!_locked)
            {               
                _play = false;                
            }
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
                // Free any other managed objects here.                
            }
            // Free any other unmanaged objects here.

            disposed = true;
        }
    }
}
