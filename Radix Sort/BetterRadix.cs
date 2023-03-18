using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonComparativeSorts
{
    static partial class NonComparativeSorts
    {
        public static void BetterRadixSort(this int[] dataset)
        {            
            // Better version (by Edden) - now with negatives.
            // Only 10 buckets, O(n + 10) extra space 
            // 10 buckets because this is a decimal (base-10) implementation 
            // (only 2 would be needed for binary, 16 would be needed for hex, etc.)

            // The algorithm sorts by each digit, so we need to loop
            // over the dataset as many times as there are digits 
            // in the largest value in the array
            var dataBounds = dataset.GetMinAndMax();
            int negativeOffset = dataBounds.Min < 0 ? dataBounds.Min : 0;
            int maxDegree = dataBounds.Max;

            int[] buckets = new int[10];
            int[] result = new int[dataset.Length];

            // Loop through each digit, up to max digits
            for (int i = 0; i < maxDegree; i++)
            {
                int divisor = (int)Math.Pow(10, i);

                // count how many numbers with each digit
                // at i-th position there are. Note, no sorting
                // is happening yet, just counting
                for (int j = 0; j < dataset.Length; j++)
                {
                    buckets[(dataset[j] - negativeOffset) / divisor % 10]++;
                }

                // this clever loop counts how many values
                // in the dataset have a digit at i-th position
                // that's smaller than index j. This is necessary
                // for placing the actual values into the result
                // array, preserving order of tied values. 
                // 
                // Each bucket will now contain the index where
                // the value who's digit at i-th position matches
                // that bucket will be placed in the result array.
                for (int j = 1; j < buckets.Length; j++)
                {
                    buckets[j] += buckets[j - 1];
                }

                // Now placing values into the result array, and 
                // decrementing the placement index in the buckets
                // array. This loop ensures the sort is stable.
                // Note, no comparisions - just placement of values.
                for (int j = dataset.Length - 1; j >= 0; j--)
                {
                    result[--buckets[(dataset[j] - negativeOffset) / divisor % 10]] = dataset[j];
                }

                // Remaining data in the buckets array must be cleared,
                // as it's no longer necessary, and will interfere with
                // the next iteration (i+1 -th digit).
                Array.Clear(buckets);

                // result contains all of our data - the last loop
                // ensures that - go through the math, it's fun!
                // copying result back to dataset - now sorted up to
                // i-th digit, and ready to repeat on i+1 -th digit.
                Array.Copy(result, dataset, result.Length);
            }
        }

    }
}
