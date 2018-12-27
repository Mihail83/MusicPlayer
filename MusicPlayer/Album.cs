using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
{
    public class Album
    {
        public byte[] Thumbnail;
        public string Name;
        public int Year;

        public Album()
        {
            this.Name = "NoName";
            this.Year = DateTime.Now.Year;            
        }
    }
}
