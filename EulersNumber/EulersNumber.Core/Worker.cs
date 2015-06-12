using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Numerics;

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
            return Algorithm(Convert.ToInt32(states["-p"]));
        }

        private BigDecimal Algorithm(BigDecimal n)
        {
            int max = Convert.ToInt32(states["-t"]);
            ThreadPool.SetMaxThreads(max, max);

            using (countdownEvent = new CountdownEvent((int)n))
            {
                for (BigDecimal i = 0; i < n; ++i)
                    ThreadPool.QueueUserWorkItem(new WaitCallback(Iteration), i);

                countdownEvent.Wait();
            }
            //Iteration(i);
            
            return Total;
        }

        private void Iteration(object p)
        {
            BigDecimal k = (BigDecimal)p;

            lock(lockObject)
            {
                Total += (3 - 4 * (k * k)) / Factorial(2 * k + 1);
            }
            countdownEvent.Signal();
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

        private readonly Dictionary<string, string> states;
        private readonly Object lockObject = new Object();
        private CountdownEvent countdownEvent;
        private const string DEFAULT_OUTPUT_FILE = "output.txt";
    }
}
