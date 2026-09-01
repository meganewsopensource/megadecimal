namespace TMegaDecimal;

internal static class DecimalMath
{
    public static decimal Round(decimal value, int precision)
    {
        return decimal.Round(
            value,
            precision,
            MidpointRounding.AwayFromZero);
    }
}