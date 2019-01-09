using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Console.WriteLine("sound has been increased");
            }           
        }

        public void VolumeDown()
        {
            if (!_locked)
            {
                Volume--;
                Console.WriteLine("sound has been reduced");
            }             
        }

        public void VolumeChange( int step)
        {
            if (!_locked)
            {

                Volume += step;
                if (step > 0)
                {
                    Console.WriteLine("sound has been increased");
                }
                else
                {
                    Console.WriteLine("sound has been reduced");
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
                        Console.WriteLine($"Player is playing:   {item} ");
                        System.Threading.Thread.Sleep(500);
                    }
                }               
            }           
        }

        public void Stop()
        {
            if (!_locked)
            {
                Console.WriteLine("Player has stopped");
                _play = false;
            }            
        }
        
        public void Add(Song adddendSong )
        {
            if (adddendSong == null)
            {
                Console.WriteLine("Песен нет");
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
                Console.WriteLine("Песен нет");
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
                Console.WriteLine("Песен нет");
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
                Console.WriteLine("Плеер разблокирован");
            }
            else
            {
                _locked = true;
                Console.WriteLine("Плеер заблокирован");
            }
        }

        public void Shuffle()
        {
            int index = 0;
            var shuffledSongs = new List<Song>();
            while (0 < _songs.Count)
            {
                index = rnd.Next(0, _songs.Count);
                shuffledSongs.Add(_songs[index]);
                _songs.RemoveAt(index);
            }                
           _songs.AddRange(shuffledSongs);            
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
    }
}
