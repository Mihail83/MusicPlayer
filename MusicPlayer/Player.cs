using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skins;

namespace MusicPlayer
{
    public class Player
    {
        private bool _locked;
        private bool _play;
        // сменить на private
        private int counter;
        private List<Song> _songs;
        private bool loop;

        ISkin skin;

        Random rnd = new Random();

        const int MIN_VOLUME = 0;
        const int MAX_VOLUME = 100;

        private int _volume;

        public int Volume
        {
            get
            {
                return _volume;
            }

            private set
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

        public Player()
        {
            counter = 0;
            skin = new ClassicSkin();
            _songs = new List<Song>();
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

        public void VolumeChange( int step)
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

        public void Play(bool loop = false)
        {
            int loopNumber = loop ? 5 : 1;
            if (_locked) return;
            else
            {                
                _play = true;

                for (int i = 0; i < loopNumber; i++)
                {
                    foreach (var item in _songs)
                    {
                        //switch (item.FieldLike)                                          //извращение??
                        //{
                        //    case null:
                        //        Console.ResetColor();
                        //        break;
                        //    case true:
                        //        Console.ForegroundColor = ConsoleColor.Green;
                        //        break;
                        //    case false:
                        //        Console.ForegroundColor = ConsoleColor.Red;
                        //        break;                            
                        //}
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
        
        public void Add(Song adddendSong )
        {
            if (adddendSong == null)
            {
                skin.Render("Песен нет\n");
            }
            else
            {
                _songs.Add(adddendSong);
            }               
        }

        public void Add(List<Song> adddendSong)
        {
            if (adddendSong == null)
            {
                skin.Render("Песен нет\n");
            }
            else
            {
                _songs.AddRange(adddendSong);                               
            }
        }
       
        public void Add(IEnumerable<Song> adddendSong)
        {
            if (adddendSong == null)
            {
                skin.Render("Песен нет\n");
            }
            else
            {
                _songs.AddRange(adddendSong);
            }
        }

        public void Remove(int RemoveIndex)
        {
            _songs.RemoveAt(RemoveIndex);        
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

        public void Shuffle()
        {
            _songs.Shuffle();      
        }

        public void SortByTitle()
        {
            var listForSort = new List<Song>();
            var nameList = new List<string>();
            foreach (var item in _songs)
            {
                nameList.Add(item.Name);
            }
            nameList.Sort();
            for (; 0 < nameList.Count;)
            {
                foreach (var item in _songs)
                {
                    if (nameList[0].Equals(item.Name))
                    {
                        listForSort.Add(item);
                        nameList.RemoveAt(0);
                        break;
                    }
                }
            }
            _songs.Clear();
            _songs.AddRange(listForSort);
        }
        public void LazyAndRightSort()  //  :-)
        {
            _songs.Sort();
        }

        //-BL8-Player4/4. FilterByGenre

        public void FilterByGenre(Artist.Genre genre)
        {
            for (int i = _songs.Count-1; i >= 0; i--)
            {
                if (_songs[i].Artist.genre != genre)
                {
                    _songs.RemoveAt(i);
                }
            }            
        }

        public void SortByGenre()
        {
            List<Song> sortedSongs = new List<Song>();
            var sortedsongsLINQ = from n in _songs orderby n.Artist.genre  select n;
            sortedSongs.AddRange(sortedsongsLINQ);           
            _songs = sortedSongs;                       
        }

        public void NewScreen()
        {
            skin.Clear();
        }
    }
}
