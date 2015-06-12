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
                "-p 500",
                "-t 4",
                "-o output.txt",
                "-q"
            };
            Worker worker = new Worker(inp);
            var s = worker.Calculate();
            System.Console.WriteLine(s);
            //WaitHandle.WaitAll();
        }
    }
}
