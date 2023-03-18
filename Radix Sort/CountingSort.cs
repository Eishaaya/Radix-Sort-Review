using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NonComparativeSorts
{
    static partial class NonComparativeSorts
    {
        //basic counting sort
        //negatives can easily be handled via offsetting by the smallest negative value so all numbers are treated as if they were >= 0
        public static void CountingSort(this uint[] dataset)
        {
            //finding out how many buckets we need
            //with a random dataset, just imagine how much space here is being wasted, it's ridiculous
            uint maxValue = dataset.GetMaxValue();
            uint[] buckets = new uint[maxValue + 1];

            //Keeping track of how many of each value (if any) exist in our dataset
            //if you notice, "duplicate" values are tracked via incrementation, which is fine if we just care about integer values alone: 
            //But, this means if we had integer keys attached to some value, we lose said value! (which is not good...for obvious reasons)
            foreach (int value in dataset)
            {
                buckets[value]++;
            }
            //looping through every bucket and placing the value corresponding to each bucket for every time we saw that value initially
            //this sorts our values since we index through the bucket array in order
            int dataIndex = 0;
            for (uint i = 0; i < buckets.Length; i++)
            {
                while (buckets[i]-- > 0)
                {
                    dataset[dataIndex++] = i;
                }
            }
        }
    }
}
