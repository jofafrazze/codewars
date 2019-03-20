using System;
using System.Collections.Generic;
using System.Linq;

namespace Codewars
{
    class Program
    {
        // Link: https://www.codewars.com/kata/55983863da40caa2c900004e
        //
        // Difficulty: 4 kyu
        // 
        static void Main(string[] args)
        {
            Console.WriteLine(Kata.NextBiggerNumber(12345));
            Console.WriteLine(Kata.NextBiggerNumber(35421));
        }
    }

    public class Kata
    {
        public static long ToLong(IEnumerable<int> list)
        {
            long sum = 0;
            foreach (int i in list)
                sum = sum * 10 + i;
            return sum;
        }
        public static long NextBiggerNumber(long n)
        {
            List<int> all = n.ToString().Select(x => int.Parse(new string(new[] { x }))).ToList();
            List<int> left = all.Take(all.Count - 1).ToList();
            List<int> right = all.Skip(all.Count - 1).Take(1).ToList();
            for (int i = left.Count - 1; i >= 0; i--)
            {
                if (left[i] < right.Last())
                {
                    int index = -1;
                    for (int k = 0; (k < right.Count) && (index < 0); k++)
                    {
                        if (left[i] < right[k])
                            index = k;
                    }
                    int tmp = left[i];
                    left.RemoveAt(i);
                    left.Add(right[index]);
                    right.RemoveAt(index);
                    right.Add(tmp);
                    right.Sort();
                    left.AddRange(right);
                    return ToLong(left);
                }
                else
                {
                    right.Add(left[i]);
                    right.Sort();
                    left.RemoveAt(i);
                }
            }
            return -1;
        }
    }
}
