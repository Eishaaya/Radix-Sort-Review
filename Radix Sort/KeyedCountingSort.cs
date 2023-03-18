using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radix_Sort
{

    interface IKeyable
    {
        public uint Key { get; }
    }
    static partial class NonComparativeSorts
    {
        //negatives can easily be handled via offsetting by the smallest negative value so all numbers are treated as if they were >= 0
        public static void KeyedCountingSort<T>(this T[] dataset) where T : IKeyable
        {
            uint maxValue = dataset.GetMaxValue();

            List<T>[] buckets = new List<T>[maxValue + 1];

            for (int i = 0; i < buckets.Length; buckets[i++] = new List<T>()) ;

            foreach (var value in dataset)
            {
                buckets[value.Key].Add(value);
            }
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
