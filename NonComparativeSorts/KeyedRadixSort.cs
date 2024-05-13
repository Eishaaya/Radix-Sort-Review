using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Radix_Sort
{
    static partial class NonComparativeSorts
    {
        
        public static void KeyedRadixSort<T>(this T[] dataset) where T : IKeyable
        {
            

            //Finding the smallest and largest keys in the dataset
            var dataBounds = dataset.GetMinAndMax();
            //this offset accounts for negatives, or decreases our total iterations in a case where we have a large minimum
            //for example: min = 1000, max = 1500, we only have a range of 500, so we only need to account for 3 digits, instead of 4!
            int minimumOffset = dataBounds.Min;
            int maxDegree = (dataBounds.Max - dataBounds.Min).DigitCount();

            //Initializing all the buckets, notice that the buckets store the values, instead of counting them (just like when we keyed counting sort)
            List<T>[] buckets = new List<T>[10];
            for (int i = 0; i < buckets.Length; buckets[i++] = new List<T>()) ;

            
            //storing our divisor so we don't need to recalculate it 
            int divisor = 1;
            //looping for every digit
            for (int i = 0; i < maxDegree; i++)
            {                             
                //placing every value into its matching bucket
                //(notice that the first item in the bucket stays first)
                for (int j = 0; j < dataset.Length; j++)
                {
                    buckets[(dataset[j].Key - minimumOffset) / divisor % 10].Add(dataset[j]);
                }

                int dataIndex = 0;
                //looping through the buckets, and placing their values back into the dataset
                //because the buckets are ordered by their index, this sorts the dataset based on whichever digit is currently being checked
                //and because the buckets store values in their original order, the sorted order based off of the lesser significant digits is preserved (this also allows the algorithm to be stable)
                for (int j = 0; j < buckets.Length; j++)
                {
                    //adding every item in a specific bucket back into the original dataset (no need for a result array, since the bucket stores the values now)
                    foreach (var item in buckets[j])
                    {
                        dataset[dataIndex++] = item;
                    }
                    //emptying the bucket for the next iteration
                    buckets[j].Clear();
                }
                //moving on to the next digit
                divisor *= 10;
            }
        }
    }
}
