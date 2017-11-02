using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParallelLibrary
{
    public class ParallelSort
    {
        public delegate int Comparison<in T>(T x, T y);

        public static void QuicksortSequential<T>(IList<T> list, Comparison<T> comparison) where T : IComparable<T>
        {
            QuicksortSequential(list, 0, list.Count - 1, comparison);
        }

        public static void QuicksortParallel<T>(IList<T> list, Comparison<T> comparison) where T : IComparable<T>
        {
            QuicksortParallel(list, 0, list.Count - 1, comparison);
        }

        private static void QuicksortSequential<T>(IList<T> list, int left, int right, Comparison<T> comparison)
            where T : IComparable<T>
        {
            if (right > left)
            {
                int pivot = Partition(list, left, right, comparison);
                QuicksortSequential(list, left, pivot - 1, comparison);
                QuicksortSequential(list, pivot + 1, right, comparison);
            }
        }

        private static void QuicksortParallel<T>(IList<T> list, int left, int right, Comparison<T> comparison)
            where T : IComparable<T>
        {
            const int SEQUENTIAL_THRESHOLD = 2048;
            if (right > left)
            {
                if (right - left < SEQUENTIAL_THRESHOLD)
                {
                    QuicksortSequential(list, left, right, comparison);
                }
                else
                {
                    int pivot = Partition(list, left, right, comparison);
                    Parallel.Invoke(new Action[] {
                        delegate {
                            QuicksortParallel(list, left, pivot - 1, comparison);
                        },
                        delegate {
                            QuicksortParallel(list, pivot + 1, right, comparison);
                        }
                    });
                }
            }
        }

        private static void Swap<T>(IList<T> list, int i, int j)
        {
            T tmp = list[i];
            list[i] = list[j];
            list[j] = tmp;
        }

        private static int Partition<T>(IList<T> list, int low, int high, Comparison<T> comparison)
            where T : IComparable<T>
        {
            int pivotPos = (high + low) / 2;
            T pivot = list[pivotPos];
            Swap(list, low, pivotPos);

            int left = low;
            for (int i = low + 1; i <= high; i++)
            {
                if (comparison(list[i], pivot) < 0)
                {
                    left++;
                    Swap(list, i, left);
                }
            }

            Swap(list, low, left);
            return left;
        }
    }
}
