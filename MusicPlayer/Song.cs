using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MusicPlayer
{
    public class Song : IComparable<Song>, IEquatable<Song>
    {
        private bool? _like;
        public int Duration;
        public string Name;
        public string Lyrics;
        public Artist Artist;
        public Album Album;
        
        
        public Song()
        {
            Duration = 1;
            Name = "Unnamed";
            Lyrics = "LaiLai";
            Artist = new Artist();
            Album = new Album();
            _like = null;
        }
        public bool? FieldLike
        {
            get
            {
                return _like;
            }

            set
            {
                if (_like==null)
                {
                    _like = value;
                }
                else if (_like !=value)
                {
                    _like = null;
                }
            }
        }
        public override string ToString()
        {
            if (this.FieldLike != null)
            {
                if (this.FieldLike == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
            }
            else
            {
                Console.ResetColor();
            }


            return $"{String.Format("{0,10}", Name.CutSongName()) } - {Lyrics} --  {Duration} sec, {Artist}   {Artist.genre} {Album} \n";
        }

        public int CompareTo(Song other)
        {           
            if (!Name.Equals(other.Name))
            {
                return Name.CompareTo(other.Name);
            }
            else
            {
                if (!(Duration == other.Duration))
                {
                    return Duration.CompareTo(other.Duration);
                }
                else
                {
                    if (!Artist.Equals(other.Artist))
                    {
                        return Artist.CompareTo(other.Artist);                        
                    }
                    else
                    {
                        return 1;
                    }
                }
            }           
        }

        public bool Equals(Song other)
        {
            if (Name.Equals(other.Name) && Duration.Equals(other.Duration) && Artist.Equals(other.Artist))
            {
                return true;
            }
            return false;
        }

        public void Like()
        {            
            FieldLike = true;
        }

        public void Dislike()
        {
            FieldLike = false;
        }
       
    }
}
