using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using System.Numerics;
using System.Diagnostics;
using System.Numerics;

namespace EulersNumber.Core
{
    public class Worker
    {
        public BigDecimal Total;

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
            return Algorithm(Convert.ToInt32(states["-p"]));
        }

        private BigDecimal Algorithm(int n)
        {
            Stopwatch watch = Stopwatch.StartNew();
            int maxThreads = Convert.ToInt32(states["-t"]);

            Thread[] threads = new Thread[maxThreads];
            int interval = n / maxThreads;

            //for (int i = 0; i < maxThreads; ++i)
            //    threads[i] = new Thread(new ThreadStart(() => ));


            using (countdown = new CountdownEvent(4))
            {
                foreach (Thread thread in threads)
                    thread.Start();

                countdown.Wait();
            }
            //for (int i = 0; i < n;)
            //{
            //    if (currentThreads < maxThreads)
            //    {
            //        ++currentThreads;
            //        Thread thread = new Thread(new ThreadStart(() => Iteration(i)));
            //        thread.Start();
            //        ++i;
            //    }
            //}
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds + " Miliseconds");
            return Total;
        }
        
        private void Iteration(int a, int b)
        {
            BigDecimal res = 0;
            for (int i = a; i < b; ++i)
            {
                Console.WriteLine(i);
                
                res += (3 - 4 * (i * i)) / Factorial(2 * i + 1);
            }

            lock (lockObject)
            {
                Total += res;
            }
            countdown.Signal();

            //Console.WriteLine(currentThreads + " " + k);
            //lock (lockObject)
            //{
            //    Total += (3 - 4 * (k * k)) / Factorial(2 * k + 1);
            //    --currentThreads;
            //}
        }

        private BigDecimal Factorial(BigDecimal k) 
        {
            if (k <= 1)
                return 1;

            if (factorialCache.ContainsKey(k))
                return factorialCache[k];

            BigDecimal result = 1;
            for (BigDecimal i = 2; i <= k; ++i)
            {
                for (BigDecimal j = i; j > 0; --j)
                {
                    if(factorialCache.ContainsKey(j))
                    {
                        i = j + 1;
                        result = factorialCache[j];
                        break;
                    }
                }
                result = result * i;
            }
            factorialCache[k] = result;
            return result;
        }

        private readonly Dictionary<string, string> states;
        private Dictionary<BigDecimal, BigDecimal> factorialCache = new Dictionary<BigDecimal, BigDecimal>();
        private readonly Object lockObject = new Object();
        private CountdownEvent countdown;
        private const string DEFAULT_OUTPUT_FILE = "output.txt";
    }
}
