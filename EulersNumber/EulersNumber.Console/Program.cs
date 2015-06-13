using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EulersNumber.Core;
using System.Numerics;
using System.IO;

namespace EulersNumber.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            string[] inp = new string[]
            {
                "-p",
                "5000",
                "-t",
                "6",
                "-o",
                "output.txt",
                "-q false"
            };
            Worker worker = new Worker(inp);
#else
            Worker worker = new Worker(args);
#endif

            var s = worker.Calculate();
        }
    }
}
