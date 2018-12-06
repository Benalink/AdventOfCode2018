using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using AdventOfCode2018;
using AdventOfCode2018.Solvers;

namespace AdventOfCode2018Tests
{
    public class Day2Tests
    {
        [Fact]
        public void Part1()
        {
            IEnumerable<string> input = File.ReadAllLines($"{AppContext.BaseDirectory}/Inputs/Day2.txt");

            int result = Solvers.Day2.Part1Solver(input);

            Assert.Equal(8398, result);
        }

        [Fact]
        public void Part2()
        {
            IEnumerable<string> input = File.ReadAllLines($"{AppContext.BaseDirectory}/Inputs/Day2.txt");

            string result = Solvers.Day2.Part2Solver(input);

            Assert.Equal("hhvsdkatysmiqjxunezgwcdpr", result);
        }
    }
}
