using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
{
    public class Song : IComparable<Song>, IEquatable<Song>
    {
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
        }
        public override string ToString()
        {
            return $"{Name} - {Lyrics} --  {Duration} sec, {Artist}  {Album} ";
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

    }
}
