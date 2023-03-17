namespace Radix_Sort
{
    internal class Program
    {
        //2mn + 20m
        static int[] RadixSort(int[] dataset, int maxDegree)
        {
            int[] buckets = new int[10];
            for (int i = 0; i < maxDegree; i++)
            {
                int currDegree = (int)Math.Pow(10, i);
                int[] result = new int[dataset.Length];
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
                for (int j = 0; j < buckets.Length; buckets[j++] = 0) ;
                dataset = result;
            }
            return dataset;
        }
        static void Main(string[] args)
        {
            Random rand = new Random(1);
            while (true)
            {
                int magnitude = rand.Next(1, 10);
                int max = (int)Math.Pow(10, magnitude);
                foreach (var item in RadixSort(new int[rand.Next(20)].Select(
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