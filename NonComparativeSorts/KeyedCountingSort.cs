using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radix_Sort
{

    public interface IKeyable
    {
        public int Key { get; }
    }
    public static partial class NonComparativeSorts
    {
        public static void KeyedCountingSort<T>(this T[] dataset) where T : IKeyable
        {
            //finding out how many buckets we need
            //with a random dataset, just imagine how much space here is being wasted, it's ridiculous
            var bounds = dataset.GetMinAndMax();
            int maxValue = bounds.Max;
            //min is being used as an offset, this accounts for negatives, or removes any unecessary buckets if our minimum is above zero
            int minValue = bounds.Min;
            List<T>[] buckets = new List<T>[maxValue + 1 - minValue];
            //initializing every list in each bucket (this makes the space cost even worse than when each index just held an integer)
            for (int i = 0; i < buckets.Length; buckets[i++] = new List<T>()) ;
            //placing every value in the bucket corresponding to its key
            //notice now that the values themselves are being properly stored
            foreach (var value in dataset)
            {
                buckets[value.Key - minValue].Add(value);
            }
            //looping through every bucket and placing the value corresponding to each bucket for every time we saw that value initially
            //this sorts our values since we index through the bucket array in order
            int dataIndex = 0;
            for (uint i = 0; i < buckets.Length; i++)
            {
                foreach (var value in buckets[i])
                {
                    dataset[dataIndex++] = value;
                }
            }
        }

    }
}
