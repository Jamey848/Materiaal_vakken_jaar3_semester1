using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pseudorandomness_AI
{
    public class Myclass
    {
        public string Name { get; set; }
        public Myclass(string name)
        {
            Name = name;
        }
    }

    public class LCG
    {
        public long a = 214013;
        public long m = (long)Math.Pow(2, 31);
        public long c = 2531011;

        long start;

        public LCG()
        {
            start = System.DateTime.Now.Ticks;
            Console.WriteLine(start);
        }

        public long next(long max)
        {
            long next = (a * start + c) % m;
            start = next;
            next %= max;
            return next;
        }
    }
}
