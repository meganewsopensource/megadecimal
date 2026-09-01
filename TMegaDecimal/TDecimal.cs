namespace TMegaDecimal;

public abstract class TDecimal
{
    protected readonly decimal _value;

    public decimal Value => _value;

    protected TDecimal(decimal value, int precision)
    {
        _value = DecimalMath.Round(
            value,
            precision);
    }

    public decimal ToDecimal() => _value;
}