using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
{
    static public class Extension
    {
        
        public static string CutSongName(this string name)
        {
            if (name.Length > 10)
            {
                return (name.Substring(0, 7) + "...");
            }
            else
            {
                return name;
            }
        }

        public static List<Song> Shuffle(this List<Song> songs )
        {
            Random rnd = new Random();

            int index = 0;
                var shuffledSongs = new List<Song>();

            for (int i = songs.Count; i > -1; i--)
            {
                index = rnd.Next(0, i);
                songs.Add(songs[index]);
                songs.RemoveAt(index);
            }
            return songs;
        }
                              
    }
}
