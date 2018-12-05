using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AdventOfCode2018;
using Xunit;

namespace AdventOfCode2018Tests
{
    public class Day4Tests
    {
        [Fact]
        public void Part1And2()
        {
            IEnumerable<string> input = File.ReadAllLines($"{AppContext.BaseDirectory}/Inputs/Day4.txt");

            (int, int) result = Solvers.Day4.Solver(input);

            Assert.Equal(65489, result.Item1);
            Assert.Equal(3852, result.Item2);
        }
    }
}
