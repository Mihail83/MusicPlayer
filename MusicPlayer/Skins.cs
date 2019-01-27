using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPlayer;

namespace Skins
{
    public interface ISkin
    { 
          void Clear();
          void Render(string text);
          void Render(Song song);
    }

    public class ClassicSkin : ISkin
    { 
        public void Clear()   //  NewScreen
        {
            Console.Clear();            
        }

        public void Render(string text)
        {
            Console.Write(text);           
        }
        public void Render(Song song)
        {
            Console.Write(song);
            Console.ResetColor();            
        }
    }
    public class ColorSkin : ISkin
    {
        Random rnd = new Random();        

        public  void Clear()   //  NewScreen
        {
            Console.Clear();           
        }

        public  void Render(string text)
        {
            Console.ForegroundColor = (ConsoleColor)rnd.Next(0, 15);
            Console.Write(text);
        }
        public  void Render(Song song)
        {
            Console.Write(song);
            Console.ResetColor();            
        }

    }


}
