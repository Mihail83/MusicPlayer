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
            using (var player = new Player())
            {
                player.SongStartedEvent += ShowInfo;
                player.SongsListChangedEvent += ShowInfo;
                player.VolumeChangedEvent += ShowInfo;
                player.PlayerLocked += ShowInfo;
                player.PlayerUnLocked += ShowInfo;
                player.PlayerStoppedEvent += ShowInfo;

               // player.PlayerStartedEvent+= PleerStart;  //    Запуск плеера

                player.Load(@"D:\миша_документы\курсы 2018\С# basic\Wav");
                System.Threading.Thread.Sleep(1000);                

                player.Play();
                player.VolumeChange(50);

                player.Play();
                player.LockButton();

                player.Play();
                player.LockButton();
                player.Stop();
                                
            }           
            
            Console.ReadKey();            
        }

        private static void ShowInfo(List<Song> songs, Song playingSong, bool locked, int volume)
        {
            Console.Clear();// remove old data

            //Render the list of songs
            foreach (var song in songs)
            {
                if (playingSong == song)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(song.Name);//Render current song in other color.
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(song.Name);
                }
            }

            //Render status bar
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Volume is: {volume}. Locked: {locked}");
            Console.ResetColor();
        }

        //private static void PleerStart(List<Song> songs, Song playingSong, bool locked, int volume)
        //{
        //    Console.WriteLine("Плеер ожидает указаний \n press any key");
        //    Console.ReadKey();
        //    ShowInfo(songs, playingSong, locked, volume);
        //}



    }
}
