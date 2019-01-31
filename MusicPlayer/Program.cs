using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            var player = new Player();
            //player.Load(@"D:\миша_документы\курсы 2018\С# basic\Wav");
            //player.Clear();
            player.LoadPlaylist(@"D:\миша_документы\курсы 2018\С# basic\Wav\test1.xml");
            player.Like(1);
            player.Dislike(0);
            player.Play();
            player.SaveAsPlaylist("test1");
            player.Stop();
            
            Console.ReadKey();            
        }      
                
        public static int SummaryDuration(IEnumerable<PlayingItem> workSongs) 
        {
            int sum=0;
            foreach (var item in workSongs)
            {
                sum += item.Duration;
            }            
            return sum;
        }
                
        public static Artist AddArtist(string art_name = "Unknown Artist")
        {
            Artist newArtist = new Artist(art_name);
            return newArtist;
        }

        public static Album AddAlbum(string alb_name = "Unknown Album", int year = 2015)
        {
            Album newAlbum = new Album();
            newAlbum.Name = alb_name;
            newAlbum.Year = year;
            return newAlbum;
        }      
    }
}
