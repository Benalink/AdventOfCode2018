using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2018
{
    public static class Solvers
    {
        public static class Day1
        {
            public static int Part1Solver(IEnumerable<int> frequencyChanges, int initialFrequency = 0)
                => frequencyChanges.Prepend(initialFrequency).Sum();

            public static int Part2Solver(IEnumerable<int> frequencyChanges, int initialFrequency = 0)
            {
                IEnumerable<int> memorizedEnumerable = frequencyChanges as int[] ?? frequencyChanges.ToArray();
                var knownFrequencies = new HashSet<int>();
                int currentFrequency = initialFrequency;
                knownFrequencies.Add(currentFrequency);

                while (true)
                {
                    foreach (int frequencyChange in memorizedEnumerable)
                    {
                        currentFrequency += frequencyChange;
                        if (!knownFrequencies.Add(currentFrequency))
                            return currentFrequency;
                    }
                }
            }
        }
    }
}
