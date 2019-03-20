using System;
using System.Numerics;

namespace Codewars
{
    class Fibonacci
    {
        // Link: https://www.codewars.com/kata/53d40c1e2f13e331fc000c26
        //
        // Difficulty: 3 kyu
        // 
        static void Main(string[] args)
        {
            Console.WriteLine(fib(0));
            Console.WriteLine(fib(1));
            Console.WriteLine(fib(2));
            Console.WriteLine(fib(3));
            Console.WriteLine(fib(4));
            Console.WriteLine(fib(5));
            Console.WriteLine(fib(6));
            Console.WriteLine(fib(-5));
            Console.WriteLine(fib(-6));
        }

        public static Tuple<BigInteger, BigInteger> FibonacciDoubling(int n)
        {
            if (n == 0)
            {
                return Tuple.Create(BigInteger.Zero, BigInteger.One);
            }
            else
            {
                // F(2k)   = F(k) [2F(k+1) − F(k)]
                // F(2k+1) = F(k+1)^2 + F(k)^2
                var half = FibonacciDoubling(n / 2);
                var a = half.Item1;
                var b = half.Item2;
                var c = a * (2 * b - a);
                var d = a * a + b * b;
                if (n % 2 == 0)
                    return Tuple.Create(c, d);
                else
                    return Tuple.Create(d, c + d);
            }
        }

        public static BigInteger fib(int n)
        {
            int k = 1;
            if ((n < 0) && ((-n) % 2 == 0))
                k = -1;
            return k * FibonacciDoubling(n).Item1;
        }
    }
}
