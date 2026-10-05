using System;

namespace Pseudorandomness_AI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // r_n+1 = (a * r_n + c) % m (--> priemgetal)
            // m - 2 

            LCG lcg = new LCG();

            Console.WriteLine(lcg.next(20));
            Console.WriteLine(lcg.next(20));
            Console.WriteLine(lcg.next(20));
            Console.WriteLine(lcg.next(20));
            Console.WriteLine(lcg.next(20));
        }
    }
}
