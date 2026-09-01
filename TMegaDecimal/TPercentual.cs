namespace TMegaDecimal;

public sealed class TPercentual : TDecimal
{
    public const int Precision = 4;

    public TPercentual(decimal value)
        : base(value, Precision)
    {
    }

    public static TPercentual Zero => new(0m);
    
    public decimal ToFactor()
    {
        return _value / 100m;
    }

    public static implicit operator TPercentual(decimal value)
        => new(value);

    public static explicit operator decimal(TPercentual value)
        => value._value;

    public static TPercentual operator +(
        TPercentual a,
        TPercentual b)
    {
        return new TPercentual(
            a._value + b._value);
    }

    public static TPercentual operator -(
        TPercentual a,
        TPercentual b)
    {
        return new TPercentual(
            a._value - b._value);
    }

    public static TPercentual operator *(
        TPercentual percentual,
        decimal fator)
    {
        return new TPercentual(
            percentual._value * fator);
    }

    public static bool operator >(
        TPercentual a,
        TPercentual b)
        => a._value > b._value;

    public static bool operator <(
        TPercentual a,
        TPercentual b)
        => a._value < b._value;

    public static bool operator >=(
        TPercentual a,
        TPercentual b)
        => a._value >= b._value;

    public static bool operator <=(
        TPercentual a,
        TPercentual b)
        => a._value <= b._value;

    public override string ToString()
    {
        return _value.ToString("F4") + "%";
    }
}