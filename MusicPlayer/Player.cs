using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skins;

namespace MusicPlayer
{
    public class VideoPlayer : GenericPlayer<Video>
    {
        public override void Play(bool loop = false)
        {
            throw new NotImplementedException();
        }
    }

    public class Player : GenericPlayer<Song>
    {        
       // private int counter;        
        
        Random rnd = new Random();        

        public Player()
        {
            //counter = 0;
            skin = new ClassicSkin();
            _playingItem = new List<Song>();
        }

        public override void Play(bool loop = false)
        {
            int loopNumber = loop ? 5 : 1;
            if (_locked) return;
            else
            {                
                _play = true;

                for (int i = 0; i < loopNumber; i++)
                {
                    foreach (var item in _playingItem)
                    {                      
                        skin.Render($"Player is playing: ");
                        skin.Render(item);
                        
                        System.Threading.Thread.Sleep(500);
                    }
                }               
            }           
        }

        public void Stop()
        {
            if (!_locked)
            {
                skin.Render("Player has stopped\n");
                _play = false;
            }            
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
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
   
    public abstract class GenericPlayer<T> where T : PlayingItem
    {
        protected bool _locked;
        protected bool _play;
        protected int _volume;
        public List<T> _playingItem;

        const int MIN_VOLUME = 0;
        const int MAX_VOLUME = 100;      

        protected ISkin skin;

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

        public void Add(T adddendSong)
        {
            if (adddendSong == null)
            {
                skin.Render("не то \n");
            }
            else
            {
                _playingItem.Add(adddendSong);
            }
        }
        
        public void Add(IEnumerable<T> adddendSong) 
        {
            if (adddendSong == null)
            {
                skin.Render("Песен нет\n");
            }
            else
            {
                _playingItem.AddRange(adddendSong);
            }
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

        public abstract void Play(bool loop = false);

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


    }
}
