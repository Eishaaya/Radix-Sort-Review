namespace Radix_Sort
{
    internal class Program
    {
        //Where 'n' is number of items, and 'm' is their max Degree
        //Runs with a time complexity of m(3n + 20), abstracting away some array operations & constnts => O(m*n)
        //max degree can also be detected algorithmically, but it's taken in for simplicity's sake
        static void RadixSort(int[] dataset, int maxDegree)
        {
            
            int[] buckets = new int[10];
            int[] result = new int[dataset.Length];
            
            for (int i = 0; i < maxDegree; i++)
            {
                int currDegree = (int)Math.Pow(10, i);
                
                for (int j = 0; j < dataset.Length; j++)
                {
                    buckets[dataset[j] / currDegree % 10]++;
                }
                for (int j = 1; j < buckets.Length; j++)
                {
                    buckets[j] += buckets[j - 1];
                }
                for (int j = dataset.Length - 1; j >= 0; j--)
                {
                    result[--buckets[dataset[j] / currDegree % 10]] = dataset[j];
                }
                Array.Clear(buckets);
                result.CopyTo(dataset, 0);
            }
        }
        static int[] FunRadix(int[] data, int max) { RadixSort(data, max); return data; }
        static void Main(string[] args)
        {
            Random rand = new Random(1);
            while (true)
            {
                int magnitude = rand.Next(1, 10);
                int max = (int)Math.Pow(10, magnitude);
                foreach (var item in FunRadix(new int[rand.Next(20)].Select(
                    (_) => { int temp = rand.Next(max); Console.WriteLine(temp); return temp; }).ToArray(), magnitude))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(item);
                }
                Console.ResetColor();
                Console.ReadKey();
            }
        }
    }
}