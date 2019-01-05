using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
{
    public class Player
    {
        private int _volume;
        private int songsCounter;
        private Dictionary<int, Song> Songs;

        const int MIN_VOLUME = 0;
        const int MAX_VOLUME = 100;

        
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
            songsCounter = 0;
        }
            

        public void VolumeUp()
        {
            Volume++;
        }

        public void VolumeDown()
        {
            Volume--;
        }

        public void VolumeChange( int step)
        {
            Volume += step;
        }

        public void Play()
        {
            Console.WriteLine($"Player is playing: {Songs[0].Name}");
        }

        public void Stop()
        {
            Console.WriteLine("Player has stopped");
        }
                
        public void Add( Song adddendSong )
        {
            if (adddendSong == null)
            {
                Console.WriteLine("Песен нет");
            }
            else
            {
                Songs.Add(songsCounter++, adddendSong);
            }
        }

    }
}
