using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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

                ControlPlayer(player);

               // player.PlayerStartedEvent+= PleerStart;  //    Запуск плеера

                //player.Load(@"D:\миша_документы\курсы 2018\С# basic\Wav");
                //System.Threading.Thread.Sleep(1000);               
                                
            }
            
            
            Console.ReadLine();            
        }

        private static void ShowInfo(List<Song> songs, Song playingSong, bool locked, int volume)
        {
            Console.Clear();// remove old data

            //Render the list of songs
            if (songs!=null)
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
            Console.WriteLine($"l  = load,  p  =  play,  s  =  stop, c  =  Clear, E  =  exit ");
        }

        //private static void PleerStart(List<Song> songs, Song playingSong, bool locked, int volume)
        //{
        //    Console.WriteLine("Плеер ожидает указаний \n press any key");
        //    Console.ReadKey();
        //    ShowInfo(songs, playingSong, locked, volume);
        //}
        public static void ControlPlayer(Player player)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            ShowInfo(null,null,false,0);

            while (true)
            {
                var ch=Console.ReadKey();
                switch (ch.KeyChar)
                {
                    case 'l':
                    case 'L':
                        Console.WriteLine("\nВведите путь к каталогу");
                        var path = Console.ReadLine();
                        player.Load(@path);
                        break;
                    case 'p':
                    case 'P':
                        Task.Run(()=>player.Play(token));
                        break;
                    case 's':
                    case 'S':
                        cts.Cancel();
                        player.Stop();
                        break;
                    case 'c':
                    case 'C':
                        player.Clear();
                        break;
                    case 'e':
                    case 'E':
                        return;

                    default:
                        Console.WriteLine("\nНекоректная команда");
                        break;
                }
            }
        }
    }
}
