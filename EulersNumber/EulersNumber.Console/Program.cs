using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EulersNumber.Core;
using System.Numerics;

namespace EulersNumber.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] inp = new string[]
            {
                "-p",
                "1000",
                "-t",
                "5",
                "-o",
                "output.txt",
                "-q"
            };
            //Worker worker = new Worker(inp);
            //var s = worker.Calculate();
            int threads = 5;
            int p = 10000;

            int step = 10000 / 7;
            int start = 0;
            int interval = step;

            for (int i = 0; i < 7; ++i)
            {
                System.Console.WriteLine(start + " " + interval);
                start += step + 1;
                if (i == 5)
                    interval = p;
                else
                    interval += step;
            }

            //System.Console.WriteLine(interval);
            //System.Console.WriteLine(s);
            //WaitHandle.WaitAll();
        }
    }
}
