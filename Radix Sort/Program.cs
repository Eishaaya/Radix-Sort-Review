using BurstTrieTypes;

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
        public int Key { get; }
        public HighQualityTestCase(Random rand, int key)
        {
            Key = key;
            key = Math.Abs(key);
            uint StringVal = 0;
            while (true)
            {
                char newChar = (char)rand.Next(char.MaxValue + 1);
                if (StringVal + newChar > key)
                {
                    Value += (char)(key - StringVal);
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

            int items = 10000;
            int min = 'a';
            int max = 'z';
            int seed = 1;

            var words = File.ReadAllLines(@"..\..\..\WordBank.txt");

            BurstTrie trie = new(100, (char)min, (char)max);

            //BST testBST = new BST();
            HashSet<string> testSet = new(items);
            Random randy = new(seed);
            HashSet<string> wat = new();

            for (int i = 0; i < items; i++)
            {
                var testValue = words[randy.Next(0, words.Length)];
                testSet.Add(testValue);
                trie.Insert(testValue);

                wat.Add(testValue);
            }
            var currItems = items = testSet.Count;
            //Random newRandy = new(seed);
            var watEnumerator = wat.GetEnumerator();
            for (int i = -1; true;)
            {
                var skip = randy.Next(1, 4);
                for (int j = 0; j < skip; j++, i++) watEnumerator.MoveNext();
                string testValue = watEnumerator.Current;

                if (i >= items) break;
                currItems--;
                if (!testSet.Contains(testValue))
                {
                    ;
                }
                testSet.Remove(testValue);
                trie.Remove(testValue);
            }


            var treeOutput = trie.GetAll();
            var testList = testSet.ToList();
            testList.Sort();

            for (int i = 0; i < Math.Max(treeOutput.Count, testList.Count); i++)
            {
                if (testList[i] != treeOutput[i] || !testList.Contains(treeOutput[i]))
                {
                    ;
                }
            }
            ;


            //var sortMe = Enumerable.Repeat(1, items)
            //                       .Select(n => new HighQualityTestCase(Random.Shared, Random.Shared.Next(min, max)))
            //                       .ToArray();

            //var sorted = sortMe.ToArray();

            ////sorted.SimpleRadixSort();
            //sorted.KeyedCountingSort();

            //bool isSorted = sortMe.OrderBy(n => n.Key)
            //                      .SequenceEqual(sorted, new HighQualityTestCase.Comparer());

            //Console.WriteLine(isSorted);

        }
    }
}