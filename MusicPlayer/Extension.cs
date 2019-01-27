using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MusicPlayer
{
    public static  class Extension
    {
        
        public static string CutPlayingItemName(this string name)
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

        public static List<T> Shuffle<T>(this List<T> playingItem )
        {
            Random rnd = new Random();

            int index = 0;
               var shuffledSongs = new List<T>();

            for (int i = playingItem.Count; i > -1; i--)
            {
                index = rnd.Next(0, i);
                playingItem.Add(playingItem[index]);
                playingItem.RemoveAt(index);
            }
            return playingItem;
        }

        
    }
}
