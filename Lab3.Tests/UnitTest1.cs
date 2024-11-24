using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Lab3.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(new string[]
        {
            "WBWBWBWB",
            "BWBWBWBW",
            "WBWBWBWB",
            "BWBWBWBW",
            "WBWBWBWB",
            "BWBWBWBW",
            "WBWBWBWB",
            "BWBWBWBW"
        }, 1)]
        [InlineData(new string[]
        {
            "WWWWWWWW",
            "WWWWWWWW",
            "WWWWWWWW",
            "WWWWWWWW",
            "WWWWWWWW",
            "WWWWWWWW",
            "WWWWWWWW",
            "WWWWWWWW"
        }, 32)]
        [InlineData(new string[]
        {
            "BWBWBWBW",
            "WBWBWBWB",
            "BWBWBWBW",
            "WBWBWBWB",
            "BWBWBWBW",
            "WBWBWBWB",
            "BWBWBWBW",
            "WBWBWBWB"
        }, 1)]
        [InlineData(new string[]
        {
            "WWBWBWBW",
            "BWBWBWBW",
            "WBWBWBWB",
            "BWBWBWBW",
            "WBWBWBWB",
            "BWBWBWBW",
            "WBWBWBWB",
            "BWBWBWBW"
        }, 7)]
        [InlineData(new string[]
        {
            "WBWBWBWW",
            "BWBWBWBW",
            "WBWBWBWB",
            "BWBWBWBW",
            "WBWBWBWB",
            "BWBWBWBW",
            "WBWBWBWB",
            "BWBWBWBW"
        }, 1)]
        [InlineData(new string[]
        {
            "WBWBWBWB",
            "WBWBWBWB",
            "WBWBWBWB",
            "WBWBWBWB",
            "WBWBWBWB",
            "WBWBWBWB",
            "WBWBWBWB",
            "WBWBWBWB"
        }, 32)]
        [InlineData(new string[]
        {
            "WBWBWBWW",
            "BWBWBBBW",
            "WBWBWWBW",
            "BWBWBWBW",
            "WBWBWBWB",
            "BWBWBWBW",
            "WBWBWBWB",
            "BWBWBWBW"
        }, 5)]
        public void TestCalculateBuilders(string[] input, int expected)
        {
            var inputList = input.ToList();
            var result = Program.CalculateBuilders(inputList);

            Assert.Equal(expected, result);
        }
    }
}
