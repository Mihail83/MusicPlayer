using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
{
    public class Playlist : IList<Song>
    {
        private Song[] array = new Song[4];

        private int _count;
        
        public int Count
        { get
            {
                return _count;
            }
        }

        public Song this[int index]
        {
            get { return array[index]; }
            set { array[index] = value; }
        }
                
        public Playlist()
        {
            _count = 0;
        }

        public IEnumerator<Song> GetEnumerator()
        {
            return new PlaylistEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new PlaylistEnumerator(this);
        }
       
        public void Add(Song value)
        {
            if (_count >= array.Length)
            {
                Array.Resize<Song>(ref array, _count + _count / 2);
            }
                array[_count] = value;
                _count++;            
        }

        public void Clear()
        {
            _count = 0;
        }

        public bool Contains(Song song)
        {
            bool result = false;
            for (int i = 0; i < Count; i++)
            {
                if (array[i].Equals(song))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public int IndexOf(Song song)
        {
            int itemindex = -1;
            for (int i = 0; i < Count; i++)
            {
                if (array[i].Equals(song))
                {
                    itemindex = i;
                    break;
                }
            }
            return itemindex;
        }
                                    
        public void Insert(int index, Song song)
        {
            if ( (index < _count) && (index >= 0))
            {

                if (_count + 1 >= array.Length)
                {                   
                    Array.Resize<Song>(ref array, _count + _count / 2);                    
                }
                _count++;
                for (int i = _count - 1; i > index; i--)
                {
                        array[i] = array[i - 1];
                }
                array[index] = song;               
            }           
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < Count)
            {
                for (int i = index; i < Count-1; i++)
                {
                    array[i] = array[i + 1];
                }
                _count--;
            }
        }

        public bool Remove(Song song)
        {
            bool result = false;

            for (int i = 0; i < Count; i++)
            {
                if (array[i].Equals(song))
                {
                    RemoveAt(i);
                    result = true;
                    break;
                } 
            }           
            return result;
        }
        
        public void CopyTo(Song[] enterArray, int arrayIndex)
        { 
            for (int i = 0; i < this.Count; i++)
            {
                enterArray[i + arrayIndex] = array[i];
            }
        }
    }

    public class PlaylistEnumerator : IEnumerator<Song>
    {
        private Playlist _collection;
        private int curIndex;
        private Song curSong;

        public PlaylistEnumerator( Playlist collection)
        {
            _collection = collection;
            curIndex = -1;
            curSong = default(Song);
        }

        public bool MoveNext()
        {
            //Avoids going beyond the end of the collection.
            if (++curIndex >= _collection.Count)
            {
                return false;
            }
            else
            {
                // Set current box to next item in collection.
                curSong = _collection[curIndex];
            }
            return true;
        }

        public void Reset() { curIndex = -1; }

        void IDisposable.Dispose() { }

        public Song Current
        {
            get { return curSong; }
        }


        object IEnumerator.Current
        {
            get { return Current; }
        }

    }

}

