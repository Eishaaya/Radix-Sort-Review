using NonComparativeSorts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radix_Sort
{
    public static partial class NonComparativeSorts
    {
        public static void BucketSort(uint[] dataset, uint bucketCount)
        {
            //Finding the largest value in the dataset
            float max = dataset.GetMaxValue() + 1;
            uint bucketRange = (uint)Math.Ceiling(max / bucketCount);

            //Setting up buckets
            List<uint>[] buckets = new List<uint>[bucketCount];
            for (int i = 0; i < buckets.Length; buckets[i++] = new()) ;
            //filling up our buckets
            foreach (var input in dataset) { buckets[(input) / bucketRange].Add(input); }
            //Sorting out buckets and unwrapping the newly sorted data back into our original array
            int currentIndex = 0;
            foreach (var bucket in buckets)
            {
                bucket.Sort(); //sorting each bucket
                for (int i = 0; i < bucket.Count; i++)
                {
                    dataset[currentIndex++] = bucket[i]; //moving data back to the datasets
                }
            }
        }

        public static void OptimizedBucketSort<T>(T[] dataset, uint bucketCount) where T : IKeyable
        {
            //Finding the smallest and largest keys in the dataset
            var dataBounds = dataset.GetMinAndMax();
            //this offset accounts for negatives, or decreases our total iterations in a case where we have a large minimum
            //for example: min = 1000, max = 1500, we only have a range of 500:
            //if we made buckets ranging from 0-1500, about ~2/3 would be completely empty (and therefore useless)
            int minimumOffset = dataBounds.Min;
            float max = (uint)(dataBounds.Max - dataBounds.Min + 1);
            uint bucketRange = (uint)Math.Ceiling(max / bucketCount);

            //Setting up buckets
            List<T>[] buckets = new List<T>[bucketCount];
            int likelyBucketPopulation = (int)Math.Ceiling(dataset.Length / (double)bucketCount);
            //C# list resizes to 16 when it adds its first item,
            //since we know generally how many items are going to be in each bucket, we can pre-allocate each list to a more reasonable size            
            for (int i = 0; i < buckets.Length; buckets[i++] = new(likelyBucketPopulation)) ;
            //filling up our buckets
            //Technically foreach is rather slow, and in truly optimized code, it would not be here, but it's more readable 
            foreach (var input in dataset) { buckets[(input.Key - minimumOffset) / bucketRange].Add(input); }
            
            int currentIndex = 0;
            foreach (var bucket in buckets)
            {
                //Due to the nature of our buckets, they are already mostly sorted
                //So, when we unwrap back into our original array, there's barely any sorting left to do
                //which makes it extremely useful with an insertion sort, since it's of similar cost to sorting each bucket individually!
                for (int i = 0; i < bucket.Count; i++)
                {
                    dataset[currentIndex++] = bucket[i]; //moving data back to the input
                }
            }
            Array.Sort(dataset);
            //However, why sort the entire array instead of just buckets?
            //There are some seriously amazing optimizations when we access information from arrays,
            //simply put, the computer predicts that when we access an array, we probably want to get more than one item:
            //so more of the array is placed into cache (think extremely fast RAM) for us to use later. 
            //This makes our one larger sort faster than our group of smaller sorts!            
        }
    }
}
