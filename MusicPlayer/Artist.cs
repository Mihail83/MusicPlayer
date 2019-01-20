using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
{
    public class Artist : IComparable<Artist>
    {
        public Genre genre;
        public string Name;
        public enum Genre 
        {
            Rock =      0b00000001,
            Pop =       0b00000010,
            Dance =     0b00000100,
            classical = 0b00001000,
            blues =     0b00010000,
            jazz =      0b00100000,
            folk =      0b01000000,
            None =      0b10000000
        }

        public Artist()
        {
            this.Name = "Default artist";
            this.genre = Genre.None;
        }

        public Artist(string name)
        {
            this.Name = name;
            this.genre = Genre.None;
        }

        public Artist(string name, Genre genre)
        {
            this.Name = name;
            this.genre = genre;
        }

        public override string ToString()
        {
            return this.Name;
        }
        public int CompareTo(Artist other)
        {            
            return Name.CompareTo(other.Name);
        }
    }
}
