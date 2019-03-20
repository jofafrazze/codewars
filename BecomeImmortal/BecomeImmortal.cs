using System;
using System.Numerics;

public static class Immortal
{
    // Link: https://www.codewars.com/kata/59568be9cc15b57637000054
    //
    // Difficulty: 1 kyu
    // 
    static void Main(string[] args)
    {
        BatchTestElderAge(20, 3, 297);
        //BatchTestElderAge(50, 3, 29832983);

        TestElderAge(8, 5, 1, 100);
        TestElderAge(8, 8, 0, 100007);
        TestElderAge(13, 3, 0, 100007);
        TestElderAge(25, 31, 0, 100007);
        TestElderAge(5, 45, 3, 1000007);
        TestElderAge(31, 39, 7, 2345);
        TestElderAge(545, 435, 342, 1000007);

        TestElderAge(2525, 3143, 0, 10002507);
        TestElderAge(2525, 3143, 1, 10002507);
        TestElderAge(2525, 3143, 2, 10002507);
        TestElderAge(2525, 3143, 3, 10002507);
        TestElderAge(2525, 3143, 3101, 10002507);
        TestElderAge(25, 31, 0, 100007);
        //TestElderAge(46350, 46340, 10901, 13719506);

        TestElderAge(28827050410L, 35165045587L, 7109602, 13719506, 5456283);

        // You need to run this test very quickly before attempting the actual tests :)
        //Assert.AreEqual((long)5456283, Immortal.ElderAge(28827050410L, 35165045587L, 7109602, 13719506));
    }

    public static void BatchTestElderAge(long f, long step, long newp)
    {
        Console.WriteLine("Batch testing with w, h and k from 0 to {0} step {1} and modulo {2}", f, step, newp);
        long failed = 0;
        for (long n = 0; n <= f * step; n += step)
        {
            for (long m = 0; m <= f * step; m += step)
            {
                for (long k = 0; k <= f * step; k += step)
                {
                    long a = ElderAge(n, m, k, newp);
                    long b = ElderAgeBruteForce(n, m, k, newp);
                    if (a != b)
                        failed++;
                }
            }
            Console.Write(".");
        }
        Console.WriteLine();
        Console.WriteLine("Result: {0} failed tests (out of {1}).\n", failed, f * f * f);
    }

    public static void TestElderAge(long n, long m, long k, long newp, long correct = -1)
    {
        long a = ElderAge(n, m, k, newp);
        long b = (correct >= 0) ? correct : ElderAgeBruteForce(n, m, k, newp);
        if (a == b)
            Console.WriteLine("Ok, {0} == {1}", a, b);
        else
            Console.WriteLine("Fail!, {0} != {1}", a, b);
    }

    public static long ElderAgeBruteForce(long n, long m, long k, long newp)
    {
        long w = Math.Max(m, n);
        long h = Math.Min(m, n);
        long sum = 0;
        for (long y = 0; y < h; y++)
        {
            for (long x = 0; x < w; x++)
            {
                long a = x ^ y;
                if (a > k)
                {
                    sum += a - k;
                    sum %= newp;
                }
            }
        }
        return sum;
    }

    /// set true to enable debug
    public static bool Debug = false;

    public static long RectangularPartSum(long w, long h, long v1, long v2, long k, long newp)
    {
        long a = v1 - k;
        long b = v2 - k;
        BigInteger n = w * (BigInteger)h;
        if (a > 0)
            return (long)(((a + b) * n / 2) % newp);
        else if (b <= 0)
            return 0;
        else
        {
            BigInteger nNotZero = n * b / (v2 - v1 + 1);
            return (long)(((1 + b) * nNotZero / 2) % newp);
        }
    }

    public static long HigestPowerOf2(long a)
    {
        long p = 1;
        while (p <= a)
            p <<= 1;
        return p >> 1;
    }

    public static long ElderAge(long n, long m, long k, long newp)
    {
        long w = Math.Max(m, n);
        long h = Math.Min(m, n);
        long wc = 0;
        long hc = 0;
        long sum = 0;
        for (long y = 0; y < h; y += hc)
        {
            hc = HigestPowerOf2(h - y);
            for (long x = 0; x < w; x += wc)
            {
                wc = HigestPowerOf2(w - x);
                long s = Math.Max(wc, hc);
                long xv = x - (x % s);
                long yv = y - (y % s);
                long v0 = xv ^ yv;
                sum += RectangularPartSum(wc, hc, v0, v0 + s - 1, k, newp);
                sum %= newp;
            }
        }
        return sum;
    }
}
