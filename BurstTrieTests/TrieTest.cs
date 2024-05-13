using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Disassemblers;

using BurstTrie;

using System.Data;
using System.Text;

using BSTTrie = BurstTrie.BSTBurstTrie.BurstTrie;
using ListTrie = BurstTrie.ListBurstTrie.BurstTrie;
using CutListTrie = BurstTrie.ListBurstTrie.CutStrings.BurstTrie;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BurstTrieTests
{
    [TestClass]
    public class TrieTest
    {

        [TestMethod]
        [DataRow(typeof(BSTTrie), 1000)]
      //  [DataRow(typeof(BSTTrie), 10000)]

        [DataRow(typeof(ListTrie), 1000)]
        [DataRow(typeof(ListTrie), 10000)]

        [DataRow(typeof(CutListTrie), 1000)]
        [DataRow(typeof(CutListTrie), 10000)]
        // [DataRow(typeof(BSTTrie), 100000)]

        public void DuplicateTest(Type trieType, int dataAmount, int seed = 1)
        {
            string val = "a";
            ITrie testTrie = (ITrie)trieType.GetConstructor(new Type[] { typeof(int), typeof(char), typeof(char) })!.Invoke(new object[] { 125, 'a', 'z' });
            for (int i = 0; i < dataAmount; i++)
            {
                testTrie.Insert(val += "a");
            }
        }

        [TestMethod]
        [DataRow(typeof(BSTTrie), 1000)]
        [DataRow(typeof(BSTTrie), 10000)]

        [DataRow(typeof(ListTrie), 1000)]
        [DataRow(typeof(ListTrie), 10000)]

        [DataRow(typeof(CutListTrie), 1000)]
        [DataRow(typeof(CutListTrie), 10000)]
        public void InOrderTest(Type trieType, int dataAmount, int seed = 1)
        {
            ITrie testTrie = (ITrie)trieType.GetConstructor(new Type[] { typeof(int), typeof(char), typeof(char) })!.Invoke(new object[] { 125, 'a', 'z' });

            StringBuilder current = new("");

            while (true)
            {
                current.Append("\0");
                for (char i = 'a'; i <= 'z'; i++)
                {
                    if (dataAmount-- == 0) return;
                    current[^1] = i;
                    testTrie.Insert(current.ToString());
                }
            }
        }

        [TestMethod]
        [DataRow(typeof(BSTTrie), 1000)]
        [DataRow(typeof(BSTTrie), 10000)]
        [DataRow(typeof(BSTTrie), 100000)]
        [DataRow(typeof(BSTTrie), 1000000)]

        [DataRow(typeof(ListTrie), 1000)]
        [DataRow(typeof(ListTrie), 10000)]
        [DataRow(typeof(ListTrie), 100000)]
        [DataRow(typeof(ListTrie), 1000000)]


        [DataRow(typeof(CutListTrie), 1000)]
        [DataRow(typeof(CutListTrie), 10000)]
        [DataRow(typeof(CutListTrie), 100000)]
        [DataRow(typeof(CutListTrie), 1000000)]


        public void StandardTest(Type trieType, int dataAmount, int seed = 1)
        {
            var words = File.ReadAllLines(@"..\..\..\..\BenchmarkDemo\BiggestWordBank.txt");

            Random randy = new(seed);


            var min = words.Min(m => m.Min());
            var max = words.Max(m => m.Max());
            ITrie testTrie = (ITrie)trieType.GetConstructor(new Type[] { typeof(int), typeof(char), typeof(char) })!.Invoke(new object[] { 125, min, max });

            for (int i = 0; i < dataAmount; i++)
            {
                var testValue = words[randy.Next(0, words.Length)];
                try
                {
                    testTrie.Insert(testValue);
                }
                catch
                {
                    ;
                }
            }


        }
    }
}