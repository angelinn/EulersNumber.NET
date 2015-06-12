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

            int step = n / maxThreads;
            int start = 0;
            int end = step;

            using (countdown = new CountdownEvent(maxThreads))
            { 
                for (int i = 0; i < maxThreads; ++i)
                {
                    threads[i] = new Thread(new ThreadStart(() => Iteration(start, end)));
                    threads[i].Start();

                    start += step + 1;

                    if (i == maxThreads - 2)
                        end = n;
                    else
                        end += step;
                }

                countdown.Wait();
            }

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
                //for (BigDecimal j = i; j > 0; --j)
                //{
                //    if(factorialCache.ContainsKey(j))
                //    {
                //        i = j + 1;
                //        result = factorialCache[j];
                //        break;
                //    }
                //}
                result = result * i;
            }
            //factorialCache[k] = result;
            return result;
        }

        private readonly Dictionary<string, string> states;
        private Dictionary<BigDecimal, BigDecimal> factorialCache = new Dictionary<BigDecimal, BigDecimal>();
        private readonly Object lockObject = new Object();
        private CountdownEvent countdown;
        private const string DEFAULT_OUTPUT_FILE = "output.txt";
    }
}
