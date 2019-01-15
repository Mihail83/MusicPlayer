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
            var playlist = new Playlist();

            var song = CreateSong("first", 10);
            song.Artist.genre = Artist.Genre.Rock;
            var song1 = CreateSong("Second", 15);
            var song2 = CreateSong("third", 20);
            playlist.Add(song);
            playlist.Add(song1);
            playlist.Add(song2);
            playlist.Add(song);
            playlist.Add(song1);
            playlist.Add(song2);
            
            song.Like();
            song1.Dislike();
            player.Add(CreateSongMassive(ref theShortestSong, ref theLongestSong));
           
            player.Add(playlist);
            player.Shuffle();


            //Console.WriteLine("Play    Shuffle    SortByTitle");
            player.Play();
            // player.Shuffle();               
            //player.Play();
            //Console.ReadLine();

            //Console.WriteLine("SortByTitle   Play    ");
                       
            //player.SortByTitle();
            //player.Play();
            //Console.ReadLine();

            //Console.WriteLine("Shuffle   Play    ");

            //player.Shuffle();
            //player.Play();
            //Console.ReadLine();

            //Console.WriteLine("LazySort   Play    ");

            
            //player.Play();

            player.Stop();
            
            Console.ReadLine();            
        }

        public static Song[] GetSongsData()
        {
            var artist = new Artist();
            artist.Name = "Powerwolf";
            artist.genre = Artist.Genre.Rock;

            var artist2 = new Artist("Lordi");
            Console.WriteLine(artist2.Name);
            Console.WriteLine(artist2.genre);

            var artist3 = new Artist("Sabaton", Artist.Genre.Rock);
            Console.WriteLine(artist3.Name);
            Console.WriteLine(artist3.genre);

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

        //public static void TraceInfo(Player player)
        //{
        //    Console.WriteLine(player._songs[0].Name);
        //    Console.WriteLine(player._songs[0].Duration);
        //    Console.WriteLine(player._songs.Count);
        //    Console.WriteLine(player.Volume);
        //}
    
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
               
        public static List<Song> CreateSongMassive(ref int theShortestSong, ref int theLongestSong)
        {
            List<Song> songList = new List<Song>();
            Random rnd = new Random();
            for (int i = 0; i < 5; i++)
            {                
                songList.Add(CreateSong("VeryUnnamed "+(i+1), rnd.Next(10, 20)));  

                theLongestSong = songList[i].Duration > theLongestSong ? songList[i].Duration : theLongestSong;
                theShortestSong = songList[i].Duration < theShortestSong ? songList[i].Duration : theShortestSong;
            }

            return songList ;                                       
        }
                
        public static int SummaryDuration(IEnumerable<Song> workSongs) 
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
