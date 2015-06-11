using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EulersNumber.Core
{
    public class Worker
    {
        public Worker(string[] input)
        {
            try
            {
                states = Parser.ParseInput(input);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Input provided.");
                throw;
            }
        }


        public decimal Calculate()
        {
            return Algorithm(Convert.ToUInt32(states["-p"]));
        }

        private decimal Algorithm(long n)
        {
            ThreadPool.SetMaxThreads(5, 5);
            for (long i = 0; i < n; ++i)
                Iteration(i);
               
                //ThreadPool.QueueUserWorkItem(new WaitCallback(Iteration), i);

            //Thread.Sleep(2000);
            return total;
        }

        private decimal total;

        private void Iteration(object p)
        {
            long k = (long)p;
            //Console.WriteLine(total);
            lock(lockObject)
            {
                var add = (decimal)(3 - 4 * (k * k)) / Factorial(2 * k + 1);
                Console.WriteLine(add);
                total += add;
            }

        }

        private long Factorial(long k)
        {
            if (k == 1)
                return 1;

            return k * Factorial(k - 1);
        }

        private readonly Dictionary<string, string> states;
        private readonly Object lockObject = new Object();
        private const string DEFAULT_OUTPUT = "output.txt";
    }
}
