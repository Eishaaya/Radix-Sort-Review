using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonComparativeSorts
{   
    static partial class NonComparativeSorts
    {
        public static void SimpleRadixSort(this uint[] values)
        {
            // Base-10 Radix sort, lol. Positive values only. LOL. 
            // very space-hungry! VERY! LOLOLOL! 10n extra space!

            // Guard
            if (values.Length == 0) return;

            // Init buckets
            int[] bucketCounts = new int[10];
            uint[,] buckets = new uint[10, values.Length];

            // The algorithm sorts by each digit, so we need to loop
            // over the dataset as many times as there are digits 
            // in the largest value in the array
            int digitCount = values.GetMaxValue().DigitCount();

            // Repeat process for each digit
            // Cost: O(n * (digitCount))
            for (int d = 0; d < digitCount; d++)
            {
                int divisor = (int)Math.Pow(10, d);

                // For each digit, the values are placed in a
                // corresponding "bucket", which is a 2D array.
                // There are far better implementations of this
                // algorithm, but this is very simple to understand.
                for (int i = 0; i < values.Length; i++)
                {
                    int index = (int)values[i] / divisor % 10;
                    buckets[index, bucketCounts[index]++] = values[i];
                }

                // The values from buckets must be copied back 
                // to the original array, in sorted order, by d-th
                // digit. The bucketCounts must be cleared, and
                // the process repeats for d+1 -th digit.
                RebuildFromBuckets(values, buckets, bucketCounts);
                Array.Clear(bucketCounts);
            }
        }
    }
}
