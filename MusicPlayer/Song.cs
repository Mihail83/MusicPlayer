using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace MusicPlayer
{
    public class Song : PlayingItem, IComparable<Song>, IEquatable<Song>
    {         
        public string Lyrics;
        public Artist Artist;
        public Album Album;

        public Song() : base()
        { }

        public Song(FileInfo playingItemInfo) : base(playingItemInfo)
        {
            Duration = 20;            
            Lyrics = "LaiLai";
            Artist = new Artist();
            Album = new Album();
            _like = null;
        }        
        
        public override string ToString()
        {
            return base.ToString() + $"  {Lyrics} , {Artist}   {Artist.genre} {Album} \n";
        }

        public int CompareTo(Song other)
        {           
            if (!Name.Equals(other.Name))
            {
                return Name.CompareTo(other.Name);
            }
            else
            {
                if (!(Duration == other.Duration))
                {
                    return Duration.CompareTo(other.Duration);
                }
                else
                {
                    if (!Artist.Equals(other.Artist))
                    {
                        return Artist.CompareTo(other.Artist);                        
                    }
                    else
                    {
                        return 1;
                    }
                }
            }           
        }

        public bool Equals(Song other)
        {
            if (Name.Equals(other.Name) && Duration.Equals(other.Duration) && Artist.Equals(other.Artist))
            {
                return true;
            }
            return false;
        }       
    }

    public class Video : PlayingItem
    {

        public Video(FileInfo playingItemInfo) : base(playingItemInfo)
        {
        }
        public Video() : base()
        { }

        public override string ToString()
        {
            return base.ToString()+"\n";
        }


    }

    public abstract class PlayingItem
    {
        protected bool? _like;
        public int Duration;
        public string Name;
        public string path;

        public PlayingItem()
        { }

        public PlayingItem(FileInfo playingItemInfo)
        {
            path = playingItemInfo.FullName;
            Name = playingItemInfo.Name;
        }

        public bool? FieldLike
        {
            get
            {
                return _like;
            }

            set
            {
                if (_like == null)
                {
                    _like = value;
                }
                else if (_like != value)
                {
                    _like = null;
                }
            }
        }

        public void Like()
        {
            FieldLike = true;
        }

        public void Dislike()
        {
            FieldLike = false;
        }
        
        public override string ToString()
        {
            if (this.FieldLike != null)
            {
                if (this.FieldLike == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
            }
            else
            {
                Console.ResetColor();
            }
            return $"{String.Format("{0,10}", Name.CutPlayingItemName()) }  -   {Duration/60} min {Duration%60} sec ";
        }
    }
}
