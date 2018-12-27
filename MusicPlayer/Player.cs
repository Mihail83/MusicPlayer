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

        public bool Playing
        {
            get
            {
                return _play;
            }
        }

        public Song[] Songs;

        public void VolumeUp()
        {
            if (_locked) return;
            else
            {
                Volume++;
                Console.WriteLine("sound has been increased");
            }         
        }

        public void VolumeDown()
        {
            if (_locked) return;
            else
            {
                Volume--;
                Console.WriteLine("sound has been reduced");
                
            }           
        }

        public void VolumeChange( int step)
        {
            if (_locked) return;
            else
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

        public void Play()
        {
            if (_locked) return;
            else
            {
                Console.WriteLine($"Player is playing: {Songs[0].Name}");
                _play = true;
            }
           
        }

        public void Stop()
        {
            if (_locked) return;
            else
            {
                Console.WriteLine("Player has stopped");
                _play = false;
            }
        }

        //B5-Player8/10
        /// /// /// /// /// /// /// /// /// /// /// /// /// /// ////// /// /// /// /// /// /// /// ///
        public void Add(params Song[] adddendSong )
        {
            if (adddendSong.Length==0)
            {
                Console.WriteLine("Песен нет");
            }
            else
                Songs = adddendSong;
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

    }
}
