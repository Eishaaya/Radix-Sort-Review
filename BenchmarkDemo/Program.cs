using BenchmarkDotNet.Running;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkDemo
{
    internal class Program
    {
        public static void Main()
        {
            //Benchmark.words = File.ReadAllLines(@"..\..\..\BiggerWordBank.txt");
            var summary = BenchmarkRunner.Run<Benchmark>();
            //Benchmark bob = new();
            //bob.Setup();
            //bob.Control();
            //bob.BSTVersion();
            //bob.ListVersion();


            //   Console.WriteLine(summary);
            Console.ReadKey();
        }
    }
}
