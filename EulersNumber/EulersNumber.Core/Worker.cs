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

        private decimal Algorithm(ulong n)
        {
            ThreadPool.SetMaxThreads(5, 5);
            for (ulong i = 1; i < n; ++i)
                ThreadPool.QueueUserWorkItem(new WaitCallback(Iteration), i);

            Thread.Sleep(1500);
            return total;
        }

        private decimal total;

        private void Iteration(object p)
        {
            ulong k = (ulong)p;
            Console.WriteLine(total);
            lock(lockObject)
            {
                total += (3 - 4 * (k * k)) / Factorial(2 * k + 1);
            }

        }

        private ulong Factorial(ulong k)
        {
            if (k == 1)
                return 1;

            return k * Factorial(k - 1);
        }

        private readonly Dictionary<string, string> states;
        private readonly Object lockObject = new Object();
    }
}
