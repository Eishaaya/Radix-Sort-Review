using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using System.Runtime.CompilerServices;

using BSTBurstTrie = BurstTrie.BSTBurstTrie.BurstTrie;
using ListBurstTrie = BurstTrie.ListBurstTrie.BurstTrie;

namespace BenchmarkDemo
{
    [MemoryDiagnoser]
    [HardwareCounters(HardwareCounter.CacheMisses)]
    public class Benchmark100k
    {
        public string[] words;        

        //public static bool started = false;
        List<string> dataSet;
        List<string> controlList;

        char min = 'a';
        char max = 'z';

        void SetupWords()
        {
            string docs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            words = File.ReadAllLines($@"{docs}\GitHub\DataStructuresCurriculum\CodeExamples\NonComparativeSorts\BenchmarkDemo\BiggestWordBank.txt");
        }

        [GlobalSetup]
        public void Setup()
        {
            SetupWords();

            int seed = 1;
            int items = 100000;
            Random randy = new(seed);


            min = words.Min(m => m.Min());
            max = words.Max(m => m.Max());

            HashSet<string> testSet = new(items);

            for (int i = 0; i < items; i++)
            {
                var testValue = words[randy.Next(0, words.Length)];
                testSet.Add(testValue.ToLower());
            }
            dataSet = testSet.ToList();
            controlList = new(dataSet);
        }
        [Benchmark]
        public void DotNetListSort() => controlList.Sort();
        
        [Benchmark]
        public void ListVersion200() => ListBurstTrie.BurstSort(dataSet, min, max, 200);
        
        [Benchmark]
        public void ListVersion125() => ListBurstTrie.BurstSort(dataSet, min, max, 125);
        
        [Benchmark]
        public void ListVersion65() => ListBurstTrie.BurstSort(dataSet, min, max, 65);

        [Benchmark]
        public void ListVersion50() => ListBurstTrie.BurstSort(dataSet, min, max, 50);

        [Benchmark]
        public void ListVersion35() => ListBurstTrie.BurstSort(dataSet, min, max, 35);
        [Benchmark]
        public void ListVersion25() => ListBurstTrie.BurstSort(dataSet, min, max, 25);

        [Benchmark]
        public void ListVersion20() => ListBurstTrie.BurstSort(dataSet, min, max, 20);
        [Benchmark]
        public void ListVersion15() => ListBurstTrie.BurstSort(dataSet, min, max, 15);
        [Benchmark]
        public void ListVersion10() => ListBurstTrie.BurstSort(dataSet, min, max, 10);
        [Benchmark]
        public void ListVersion1() => ListBurstTrie.BurstSort(dataSet, min, max, 1);

        [Benchmark]
        public void BSTVersion200() => BSTBurstTrie.BurstSort(dataSet, min, max, 200);
        
        [Benchmark(Baseline = true)]
        public void BSTVersion125() => BSTBurstTrie.BurstSort(dataSet, min, max, 125);
        
        [Benchmark]
        public void BSTVersion65() => BSTBurstTrie.BurstSort(dataSet, min, max, 65);

        [Benchmark]
        public void BSTVersion50() => BSTBurstTrie.BurstSort(dataSet, min, max, 50);

        [Benchmark]
        public void BSTVersion35() => BSTBurstTrie.BurstSort(dataSet, min, max, 35);
        [Benchmark]
        public void BSTVersion25() => BSTBurstTrie.BurstSort(dataSet, min, max, 25);
        [Benchmark]
        public void BSTVersion20() => BSTBurstTrie.BurstSort(dataSet, min, max, 20);
        [Benchmark]
        public void BSTVersion15() => BSTBurstTrie.BurstSort(dataSet, min, max, 15);
        [Benchmark]
        public void BSTVersion10() => BSTBurstTrie.BurstSort(dataSet, min, max, 10);
        [Benchmark]
        public void BSTVersion1() => BSTBurstTrie.BurstSort(dataSet, min, max, 1);
    }

    public class Benchmark10k
    {
        public string[] words;

        //public static bool started = false;
        List<string> dataSet;
        List<string> controlList;

        char min = 'a';
        char max = 'z';

        void SetupWords()
        {
            string docs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            words = File.ReadAllLines($@"{docs}\GitHub\DataStructuresCurriculum\CodeExamples\NonComparativeSorts\BenchmarkDemo\BiggestWordBank.txt");
        }

        [GlobalSetup]
        public void Setup()
        {
            SetupWords();

            int seed = 1;
            int items = 100000;
            Random randy = new(seed);


            min = words.Min(m => m.Min());
            max = words.Max(m => m.Max());

            HashSet<string> testSet = new(items);

            for (int i = 0; i < items; i++)
            {
                var testValue = words[randy.Next(0, words.Length)];
                testSet.Add(testValue.ToLower());
            }
            dataSet = testSet.ToList();
            controlList = new(dataSet);
        }
        [Benchmark]
        public void DotNetListSort() => controlList.Sort();

        [Benchmark]
        public void ListVersion200() => ListBurstTrie.BurstSort(dataSet, min, max, 200);

        [Benchmark]
        public void ListVersion125() => ListBurstTrie.BurstSort(dataSet, min, max, 125);

        [Benchmark]
        public void ListVersion65() => ListBurstTrie.BurstSort(dataSet, min, max, 65);

        [Benchmark]
        public void ListVersion50() => ListBurstTrie.BurstSort(dataSet, min, max, 50);

        [Benchmark]
        public void ListVersion35() => ListBurstTrie.BurstSort(dataSet, min, max, 35);
        [Benchmark]
        public void ListVersion25() => ListBurstTrie.BurstSort(dataSet, min, max, 25);

        [Benchmark]
        public void ListVersion20() => ListBurstTrie.BurstSort(dataSet, min, max, 20);
        [Benchmark]
        public void ListVersion15() => ListBurstTrie.BurstSort(dataSet, min, max, 15);
        [Benchmark]
        public void ListVersion10() => ListBurstTrie.BurstSort(dataSet, min, max, 10);
        [Benchmark]
        public void ListVersion1() => ListBurstTrie.BurstSort(dataSet, min, max, 1);

        [Benchmark]
        public void BSTVersion200() => BSTBurstTrie.BurstSort(dataSet, min, max, 200);

        [Benchmark(Baseline = true)]
        public void BSTVersion125() => BSTBurstTrie.BurstSort(dataSet, min, max, 125);

        [Benchmark]
        public void BSTVersion65() => BSTBurstTrie.BurstSort(dataSet, min, max, 65);

        [Benchmark]
        public void BSTVersion50() => BSTBurstTrie.BurstSort(dataSet, min, max, 50);

        [Benchmark]
        public void BSTVersion35() => BSTBurstTrie.BurstSort(dataSet, min, max, 35);
        [Benchmark]
        public void BSTVersion25() => BSTBurstTrie.BurstSort(dataSet, min, max, 25);
        [Benchmark]
        public void BSTVersion20() => BSTBurstTrie.BurstSort(dataSet, min, max, 20);
        [Benchmark]
        public void BSTVersion15() => BSTBurstTrie.BurstSort(dataSet, min, max, 15);
        [Benchmark]
        public void BSTVersion10() => BSTBurstTrie.BurstSort(dataSet, min, max, 10);
        [Benchmark]
        public void BSTVersion1() => BSTBurstTrie.BurstSort(dataSet, min, max, 1);


    }

    public class Benchmark1k
    {
        public string[] words;

        //public static bool started = false;
        List<string> dataSet;
        List<string> controlList;

        char min = 'a';
        char max = 'z';

        void SetupWords()
        {
            string docs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            words = File.ReadAllLines($@"{docs}\GitHub\DataStructuresCurriculum\CodeExamples\NonComparativeSorts\BenchmarkDemo\BiggestWordBank.txt");
        }

        [GlobalSetup]
        public void Setup()
        {
            SetupWords();

            int seed = 1;
            int items = 1000;
            Random randy = new(seed);


            min = words.Min(m => m.Min());
            max = words.Max(m => m.Max());

            HashSet<string> testSet = new(items);

            for (int i = 0; i < items; i++)
            {
                var testValue = words[randy.Next(0, words.Length)];
                testSet.Add(testValue.ToLower());
            }
            dataSet = testSet.ToList();
            controlList = new(dataSet);
        }
        [Benchmark]
        public void DotNetListSort() => controlList.Sort();

        [Benchmark]
        public void ListVersion200() => ListBurstTrie.BurstSort(dataSet, min, max, 200);

        [Benchmark]
        public void ListVersion125() => ListBurstTrie.BurstSort(dataSet, min, max, 125);

        [Benchmark]
        public void ListVersion65() => ListBurstTrie.BurstSort(dataSet, min, max, 65);

        [Benchmark]
        public void ListVersion50() => ListBurstTrie.BurstSort(dataSet, min, max, 50);

        [Benchmark]
        public void ListVersion35() => ListBurstTrie.BurstSort(dataSet, min, max, 35);
        [Benchmark]
        public void ListVersion25() => ListBurstTrie.BurstSort(dataSet, min, max, 25);

        [Benchmark]
        public void ListVersion20() => ListBurstTrie.BurstSort(dataSet, min, max, 20);
        [Benchmark]
        public void ListVersion15() => ListBurstTrie.BurstSort(dataSet, min, max, 15);
        [Benchmark]
        public void ListVersion10() => ListBurstTrie.BurstSort(dataSet, min, max, 10);
        [Benchmark]
        public void ListVersion1() => ListBurstTrie.BurstSort(dataSet, min, max, 1);

        [Benchmark]
        public void BSTVersion200() => BSTBurstTrie.BurstSort(dataSet, min, max, 200);

        [Benchmark(Baseline = true)]
        public void BSTVersion125() => BSTBurstTrie.BurstSort(dataSet, min, max, 125);

        [Benchmark]
        public void BSTVersion65() => BSTBurstTrie.BurstSort(dataSet, min, max, 65);

        [Benchmark]
        public void BSTVersion50() => BSTBurstTrie.BurstSort(dataSet, min, max, 50);

        [Benchmark]
        public void BSTVersion35() => BSTBurstTrie.BurstSort(dataSet, min, max, 35);
        [Benchmark]
        public void BSTVersion25() => BSTBurstTrie.BurstSort(dataSet, min, max, 25);
        [Benchmark]
        public void BSTVersion20() => BSTBurstTrie.BurstSort(dataSet, min, max, 20);
        [Benchmark]
        public void BSTVersion15() => BSTBurstTrie.BurstSort(dataSet, min, max, 15);
        [Benchmark]
        public void BSTVersion10() => BSTBurstTrie.BurstSort(dataSet, min, max, 10);
        [Benchmark]
        public void BSTVersion1() => BSTBurstTrie.BurstSort(dataSet, min, max, 1);


    }
}