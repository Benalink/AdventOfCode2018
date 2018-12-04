using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2018
{
    public class Solvers
    {
        public int Day1Part1Solver(IEnumerable<int> frequencyChanges, int initialFrequency = 0) 
            => frequencyChanges.Prepend(initialFrequency).Sum();

        public int Day1Part2Solver(IEnumerable<int> frequencyChanges, int initialFrequency = 0)
        {
            IEnumerable<int> memEnumerable = frequencyChanges as int[] ?? frequencyChanges.ToArray();
            var knownFrequencies = new HashSet<int>();
            dynamic duplicate = new { Duplicate = false };
            int currentFrequency = initialFrequency;
            knownFrequencies.Add(currentFrequency);

            while (!duplicate.Duplicate)
            {
                //Omg it's not pure!
                duplicate = memEnumerable.Select(frequencyChange => new
                    {
                        Value = currentFrequency += frequencyChange,
                        Duplicate = !knownFrequencies.Add(currentFrequency)
                    })
                    .SkipWhile(frequency => !frequency.Duplicate).FirstOrDefault() ?? duplicate;
            }

            return duplicate.Value;
        }
    }
}
