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
            int theShortestSong = 100000;
            int theLongestSong = 0;

            var player = new Player();
            //          player.Volume = 20;
            //player.Songs = GetSongsData();           




            //B5-Player7/10
            /// /// /// /// /// /// /// /// /// /// /// /// /// /// ////// /// /// /// /// /// /// /// ///
            Console.WriteLine($" самая  короткая {theShortestSong}  самая длинная {theLongestSong}");

            var testSongsMassive = CreateSongMassive(ref theShortestSong, ref theLongestSong);
            var testSong1 = CreateSong("Lya-lya-lya ", 115);
            var testSong2 = CreateSong("Bla-Bla-Bla ", 203);

            Console.WriteLine($" самая  короткая {theShortestSong}  самая длинная {theLongestSong}");

            //B5-Player8/10
            player.Add(testSong1);
            player.Add(testSong1, testSong2);
            player.Add(testSongsMassive);


            Console.WriteLine($"общая длительность песен {SummaryDuration(player.Songs)}");
            Console.WriteLine($" самая  короткая {theShortestSong}  самая длинная {theLongestSong}");

            //    B5-Player10/10 defaultAndNamedParametrs    /// /// /// /// /// /// /// /// /// /// ////// /// /// /// /// /// /// /// ///
            var testArtist = AddArtist();
            Console.WriteLine(testArtist.Name);

            var testAlbum = AddAlbum();
            Console.WriteLine($"{testAlbum.Name}   {testAlbum.Year}");
            
            var testAlbum1 = AddAlbum(year:2010, alb_name:"GOLD");
            Console.WriteLine($"{testAlbum1.Name}   {testAlbum1.Year}");

            var testAlbum2 = AddAlbum("best", 1995);
            Console.WriteLine($"{testAlbum2.Name}   {testAlbum2.Year}");


            /*TraceInfo(player);

            player.Play();
            player.VolumeUp();
            Console.WriteLine(player.Volume);

            player.VolumeChange(-300);
            Console.WriteLine(player.Volume);

            player.VolumeChange(300);
            Console.WriteLine(player.Volume);
                        
            player.Stop();*/

            Console.ReadLine();
        }

        public static Song[] GetSongsData()
        {
            var artist = new Artist();
            artist.Name = "Powerwolf";
            artist.Genre = "Metal";

            var artist2 = new Artist("Lordi");
            Console.WriteLine(artist2.Name);
            Console.WriteLine(artist2.Genre);

            var artist3 = new Artist("Sabaton", "Rock");
            Console.WriteLine(artist3.Name);
            Console.WriteLine(artist3.Genre);

            var album = new Album();
            album.Name = "New Album";
            album.Year = 2018;

            var song = new Song()
            {
                Duration = 100,
                Name = "New song",
                Album = album,
                Artist = artist
            };

            return new Song[] {song};
        }

        public static void TraceInfo(Player player)
        {
            Console.WriteLine(player.Songs[0].Artist.Name);
            Console.WriteLine(player.Songs[0].Duration);
            Console.WriteLine(player.Songs.Length);
            Console.WriteLine(player.Volume);
        }

        /// /// /// /// /// /// /// /// /// /// /// /// /// /// ////// /// /// /// /// /// /// /// ///
        /// B5-Player6/10
        /// 
        public static Song CreateSong()
        {
            Song onesong = new Song();
            onesong.Duration= 60;
            onesong.Name = "Random Name";
            onesong.Artist = new Artist();
            onesong.Album= new Album();

            return onesong;
        }

        public static Song CreateSong(string name)
        {
            Song onesong = CreateSong();
            onesong.Name = name;

            return onesong;
        }

        public static Song CreateSong(string name, int duration)
        {
            Song onesong = CreateSong(name);
            onesong.Duration = duration;    
            
            return onesong; 
        }

        //    B5-Player7/10 OutRefParameters
        /// /// /// /// /// /// /// /// /// /// /// /// /// /// ////// /// /// /// /// /// /// /// ///
        public static Song[] CreateSongMassive(ref int theShortestSong, ref int theLongestSong)
        {
            Song[] songList= new Song[5];
            Random rnd = new Random();
            for (int i = 1; i < 6; i++)
            {                
                songList[i - 1] =CreateSong("Unnamed "+i, rnd.Next(120, 300));

                theLongestSong = songList[i - 1].Duration > theLongestSong ? songList[i - 1].Duration : theLongestSong;
                theShortestSong = songList[i - 1].Duration < theShortestSong ? songList[i - 1].Duration : theShortestSong;
            }

            return songList ;
                                        // return new Song[] { songList[0], songList[1], songList[2], songList[3], songList[4]};
        }

        //
        public static int SummaryDuration(params Song[] workSongs) 
        {
            int sum=0;
            for (int i = 0; i < workSongs.Length; i++)
            {
                sum += workSongs[i].Duration;
            }
            return sum;
        }

        //    B5-Player10/10 defaultAndNamedParametrs    /// /// /// /// /// /// /// /// /// /// ////// /// /// /// /// /// /// /// ///
        /// /// /// /// /// /// /// /// /// /// /// /// /// /// ////// /// /// /// /// /// /// /// ///
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
