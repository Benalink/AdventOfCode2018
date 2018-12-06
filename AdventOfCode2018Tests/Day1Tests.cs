using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using AdventOfCode2018.Solvers;

namespace AdventOfCode2018Tests
{
    public class Day1Tests
    {
        [Fact]
        public void Part1()
        {
            IEnumerable<int> input = File.ReadAllLines($"{AppContext.BaseDirectory}/Inputs/Day1.txt")
                .Select(int.Parse);

            int result = Solvers.Day1.Part1Solver(input);

            Assert.Equal(518, result);
        }

        [Fact]
        public void Part2()
        {
            IEnumerable<int> input = File.ReadAllLines($"{AppContext.BaseDirectory}/Inputs/Day1.txt")
                .Select(int.Parse);

            int result = Solvers.Day1.Part2Solver(input);

            Assert.Equal(72889, result);
        }
    }
}
