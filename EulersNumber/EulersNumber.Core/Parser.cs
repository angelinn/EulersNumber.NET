using System;
using System.Collections.Generic;
using System.Linq;

namespace EulersNumber.Core
{
    public static class Parser
    {
        public static Dictionary<string, string> ParseInput(string[] input)
        {
            Dictionary<string, string> states = new Dictionary<string, string>();
            FillStates(states);

            for (int i = 0; i < input.Length; ++i)
                if (i % 2 == 0)
                    states[input[i]] = String.Empty;
                else
                    states[input[i - 1]] = input[i];

            return states;
        }

        private static void FillStates(Dictionary<string, string> states)
        {
            states.Add("-p", String.Empty);
            states.Add("-t", String.Empty);
            states.Add("-o", String.Empty);
            states.Add("-q", String.Empty);
        }
    }
}
