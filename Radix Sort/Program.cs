using Radix_Sort;

using System;
using System.Collections.Generic;
using System.Linq;

namespace NonComparativeSorts
{
   
    class HighQualityTestCase : IKeyable
    {
        public struct Comparer : IEqualityComparer<HighQualityTestCase>
        {
            bool IEqualityComparer<HighQualityTestCase>.Equals(HighQualityTestCase? x, HighQualityTestCase? y) => x.Key == y.Key;

            int IEqualityComparer<HighQualityTestCase>.GetHashCode(HighQualityTestCase obj) => obj.Key.GetHashCode();
        }
        public string Value { get; }
        public uint Key { get; }
        public HighQualityTestCase(Random rand, uint key)
        {
            Key = key;
            uint StringVal = 0;
            while (true)
            {
                char newChar = (char)rand.Next(char.MaxValue + 1);
                if (StringVal + newChar > Key)
                {
                    Value += (char)(Key - StringVal);
                    return;
                }
                Value += newChar;
                StringVal += newChar;
            }
        }
        public override string ToString() => Value;
    }

    class Program
    {
        static void Main(string[] args)
        {
            int items = 20;
            int min = 1;
            int max = int.MaxValue;

            var sortMe = Enumerable.Repeat(1, items)
                                   .Select(n => new HighQualityTestCase(Random.Shared, (uint)Random.Shared.Next(min, max)))
                                   .ToArray();

            var sorted = sortMe.ToArray();

            //sorted.SimpleRadixSort();
            sorted.KeyedCountingSort();

            bool isSorted = sortMe.OrderBy(n => n.Key)
                                  .SequenceEqual(sorted, new HighQualityTestCase.Comparer());

            Console.WriteLine(isSorted);

        }
    }
}