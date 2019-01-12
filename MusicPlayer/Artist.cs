﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
{
    public class Artist : IComparable<Artist>
    {
        public string Genre;
        public string Name;

        public Artist()
        {
            this.Name = "Default artist";
            this.Genre = "Default genre";
        }

        public Artist(string name)
        {
            this.Name = name;
            this.Genre = "Default genre";
        }

        public Artist(string name, string genre)
        {
            this.Name = name;
            this.Genre = genre;
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
