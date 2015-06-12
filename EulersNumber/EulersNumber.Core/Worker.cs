using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics;

namespace EulersNumber.Core
{
    public class Worker
    {
        public BigDecimal Total { get; private set; }

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


        public BigDecimal Calculate()
        {
            int maxThreads = Convert.ToInt32(states["-t"]);
            ThreadPool.SetMaxThreads(maxThreads, maxThreads);


            return Algorithm(Convert.ToInt32(states["-p"]));
        }

        private BigDecimal Algorithm(BigDecimal n)
        {
            Stopwatch watch = Stopwatch.StartNew();
            using (countdownEvent = new CountdownEvent((int)n))
            {
                for (BigDecimal i = 0; i < n; ++i)
                    ThreadPool.QueueUserWorkItem(new WaitCallback(Iteration), i);

                countdownEvent.Wait();
            }

            //Iteration(i);
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds + " Miliseconds");
            return Total;
        }

        private void Iteration(object p)
        {
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " started.");
            BigDecimal k = (BigDecimal)p;
            
            lock(lockObject)
            {
                Total += (3 - 4 * (k * k)) / Factorial(2 * k + 1);
            }
            countdownEvent.Signal();

            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " finished.");
            //int available, s;
            //ThreadPool.GetAvailableThreads(out available, out s);

        }

        private BigDecimal Factorial(BigDecimal k) 
        {
            if (k <= 1)
                return 1;

            BigDecimal result = 1;
            for (BigDecimal i = 2; i <= k; ++i)
                result = result * i;

            return result;
        }

        private int currentThreads;
        private readonly Dictionary<string, string> states;
        private readonly Object lockObject = new Object();
        private CountdownEvent countdownEvent;
        private const string DEFAULT_OUTPUT_FILE = "output.txt";
    }
}
