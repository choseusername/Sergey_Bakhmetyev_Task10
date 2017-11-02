using System;
using System.Collections.Generic;
using System.Linq;
using ParallelLibrary;
using System.Threading;

namespace Task3
{
    class Program
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static int StringComparisonDelegate(string x, string y)
        {
            if (x.Length != y.Length)
                return x.Length - y.Length;
            else
                return x.CompareTo(y);
        }

        private static void SortStringList(IList<string> strings, ParallelSort.Comparison<string> comparisonDelegate)
        {
            ParallelSort.QuicksortParallel(strings, comparisonDelegate);
        }

        static void Main(string[] args)
        {
            List<string> strings = new List<string>();
            for (int i = 0; i < 10; ++i)
                strings.Add(RandomString(random.Next(100)));

            Action callback = () =>
            {
                Finish("Sort thread finished.");
                foreach (var item in strings)
                    Console.WriteLine(item);
            };

            Thread sortThread = new Thread(() => SortRun(strings, callback));
            sortThread.Start();

            Console.ReadKey();
        }
        static void SortRun(IList<string> strings, Action callback)
        {
            SortStringList(strings, StringComparisonDelegate);
            callback();
        }

        static void Finish(string message)
        {
            Console.WriteLine(message);
        }
    }
}
