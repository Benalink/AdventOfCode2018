using System;
using System.Collections;
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

        public static class Day2
        {
            public static int Part1Solver(IEnumerable<string> boxIds)
            {
                var containsInfoEnumerable = boxIds.Select(id => GetContainsInfo(id.GroupBy(character => character))).ToArray();
                var containsTwoCount = containsInfoEnumerable.Count(o => o.ContainsTwo);
                var containsThreeCount = containsInfoEnumerable.Count(o => o.ContainsThree);

                return containsTwoCount * containsThreeCount;

                ContainsInfo GetContainsInfo(IEnumerable<IGrouping<char, char>> charcterGroups)
                {
                    var containsInfo = new ContainsInfo();
                    foreach (var group in charcterGroups)
                    {
                        var count = group.Count();
                        if (count == 2)
                        {
                            containsInfo.ContainsTwo = true;
                        }
                        else if (count == 3)
                        {
                            containsInfo.ContainsThree = true;
                        }
                    }
                    return containsInfo;
                }
            }

            public static string Part2Solver(IEnumerable<string> boxIds)
            {
                string result = null;

                var memorisedBoxIds = boxIds as string[] ?? boxIds.ToArray();
                foreach (string currentId in memorisedBoxIds)
                {
                    foreach (var comparisonId in memorisedBoxIds.Except(Enumerable.Repeat(currentId, 1)))
                    {
                        var length = currentId.Length;
                        var zipped = currentId.Zip(comparisonId,
                                (currentChar, comparisonChar) => new {Character = currentChar, Shared = currentChar == comparisonChar})
                            .Where(o => o.Shared).ToArray();
                        var sharedCount = zipped.Length;

                        if (length - sharedCount == 1)
                        {
                            result = String.Concat(zipped.Select(o => o.Character));
                            break;
                        }
                    }
                }

                return result;
            }

            internal struct ContainsInfo
            {
                public bool ContainsTwo { get; set; }
                public bool ContainsThree { get; set; }
            }
        }
    }
}
