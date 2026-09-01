using Xunit;
using Assert = Xunit.Assert;

namespace TestMegaDecimal;

public class TestsDecimalMath
{
    [Theory]
    [InlineData(3.001, 2, 3)]
    [InlineData(3.005, 2, 3)]
    [InlineData(3.999, 2, 4)]
    [InlineData(10.123456, 2, 10.12)]
    [InlineData(10.123456, 4, 10.1235)]
    [InlineData(3.111, 1, 3.1)]
    public void TestRound(decimal value, int precision, decimal expected)
    {
        decimal rounded = Math.Round(value, precision);

        Assert.Equal(expected, rounded);
    }
}