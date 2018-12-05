using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using AdventOfCode2018;

namespace AdventOfCode2018Tests
{
    public class Day3Tests
    {
        [Fact]
        public void Part1()
        {
            IEnumerable<string> input = File.ReadAllLines($"{AppContext.BaseDirectory}/Inputs/Day3.txt");

            int result = Solvers.Day3.Part1Solver(input);

            Assert.Equal(104241, result);
        }

        [Fact]
        public void Part2()
        {
            IEnumerable<string> input = File.ReadAllLines($"{AppContext.BaseDirectory}/Inputs/Day3.txt");

            int result = Solvers.Day3.Part2Solver(input);

            Assert.Equal(806, result);
        }
    }
}
