global using static NonComparativeSorts.Extensions;

using Radix_Sort;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonComparativeSorts
{
    static class Extensions
    {
        public static uint GetMaxValue(this uint[] values)
        {
            uint maxValue = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] > maxValue)
                {
                    maxValue = values[i];
                }
            }

            return maxValue;
        }

        public static int GetMaxValue<T>(this T[] values) where T : IKeyable
        {
            int maxValue = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i].Key > maxValue)
                {
                    maxValue = values[i].Key;
                }
            }

            return maxValue;
        }

        public static (int Max, int Min) GetMinAndMax(this int[] values)
        {
            int maxValue = int.MinValue;
            int minValue = int.MaxValue;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] > maxValue)
                {
                    maxValue = values[i];
                }
                if (values[i] < minValue)
                {
                    minValue = values[i];
                }
            }

            return (maxValue, minValue);
        }
        public static (int Max, int Min) GetMinAndMax<T>(this T[] values) where T : IKeyable
        {
            int maxValue = int.MinValue;
            int minValue = int.MaxValue;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i].Key > maxValue)
                {
                    maxValue = values[i].Key;
                }
                if (values[i].Key < minValue)
                {
                    minValue = values[i].Key;
                }
            }

            return (maxValue, minValue);
        }

        public static int DigitCount(this uint value)
        {
            int digitCount = 0;

            do
            {
                value /= 10;
                digitCount++;
            } while (value > 0);

            return digitCount;
        }

        public static int DigitCount(this int value)
        {
            int digitCount = 0;

            do
            {
                value /= 10;
                digitCount++;
            } while (value > 0);

            return digitCount;
        }

        public static void RebuildFromBuckets(uint[] values, uint[,] buckets, int[] bucketCounts)
        {
            int index = 0;
            for (int b = 0; b < buckets.GetLength(0); b++)
            {
                for (int i = 0; i < bucketCounts[b]; i++)
                {
                    values[index++] = buckets[b, i];
                }
            }
        }
    }
}
