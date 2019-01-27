using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPlayer;

namespace Skins
{
    public abstract class Skin
    {
        public Skin() { }
        
        public abstract void Clear();
        public abstract void Render(string text);
        public abstract void Render(Song song);
    }

    public class ClassicSkin : Skin
    {
        public ClassicSkin():base()
        { }

        public override void Clear()   //  NewScreen
        {
            Console.Clear();            
        }

        public override void Render(string text)
        {
            Console.Write(text);           
        }
        public override void Render(Song song)
        {
            Console.Write(song);
            Console.ResetColor();            
        }
    }
    public class ColorSkin : Skin
    {
        Random rnd = new Random();
        
        public ColorSkin():base()
        {           
        }

        public override void Clear()   //  NewScreen
        {
            Console.Clear();           
        }

        public override void Render(string text)
        {
            Console.ForegroundColor = (ConsoleColor)rnd.Next(0, 15);
            Console.Write(text);
        }
        public override void Render(Song song)
        {
            Console.Write(song);
            Console.ResetColor();            
        }

    }


}
