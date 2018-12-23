﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
{
    public class Song
    {
        public int Duration;
        public string Name;
        public Artist Artist;
        public Album Album;

        public Song()
        {
            Duration = 0;
            Name = "Unnamed";
            Artist = new Artist();
            Album = new Album();
        }
    }
}
