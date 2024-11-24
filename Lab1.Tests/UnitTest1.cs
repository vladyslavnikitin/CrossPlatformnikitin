using Xunit;

namespace Lab1.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(5, 15, "10117", "97111")]
        [InlineData(10, 1, "NO SOLUTION", "NO SOLUTION")]
        [InlineData(6, 20, "100111", "991111")]
        [InlineData(4, 10, "1114", "7711")]
        [InlineData(7, 30, "1000011", "9999111")]
        public void TestFindMinMaxNumber(int n, int k, string expectedMin, string expectedMax)
        {
            var result = Program.FindMinMaxNumber(n, k);
            Assert.Equal(expectedMin, result.Min);
            Assert.Equal(expectedMax, result.Max);
        }
    }
}
