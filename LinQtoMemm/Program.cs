using System.Collections;

namespace LinQtoMemm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] ints = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            MyIntergerCollection ok = new MyIntergerCollection(ints);
            foreach (int i in ok)
            {
                Console.WriteLine(i);
            }

            foreach (int i in GetNumber())
            {
                Console.WriteLine(i);
            }
            IEnumerable<int> GetNumber()
            {
                yield return 5;
                yield return 10;
                yield return 15;
            }
        }


    }

    public class MyIntergerCollection : IEnumerable
    {
        private readonly int[] _listInt;

        public MyIntergerCollection(int[] listInt)
        {
            _listInt = listInt;
        }

        public IEnumerator GetEnumerator()
        {
            return new MyIntergerEnumerator(_listInt);
        }
    }

    public class MyIntergerEnumerator : IEnumerator
    {
        private readonly int[] _listInt;
        private int _currentIndex;

        public MyIntergerEnumerator(int[] listInt)
        {
            _listInt = listInt;
            _currentIndex = _listInt.Length;
        }

        public object Current
        {
            get
            {
                return _listInt[_currentIndex];
            }
        }

        public bool MoveNext()
        {
            _currentIndex--;

            return _currentIndex >= 0;
        }

        public void Reset()
        {
            _currentIndex = _listInt.Length;
        }
    }

}
