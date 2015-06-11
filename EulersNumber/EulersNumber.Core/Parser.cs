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

            List<List<string>> splitted = new List<List<string>>();

            foreach (string str in input)
                splitted.Add(new List<string>(str.Split(new char[] { ' ' })));

            foreach (List<string> pair in splitted)
                states[pair.First()] = pair.Last();

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
