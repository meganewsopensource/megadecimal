namespace TMegaDecimal;

public sealed class TPreco : TDecimal
{
    public const int Precision = 6;

    public TPreco(decimal value)
        : base(value, Precision)
    {
    }

    public static TPreco Zero => new(0m);

    public static implicit operator TPreco(decimal value)
        => new(value);

    public static explicit operator decimal(TPreco value)
        => value._value;

    public static TPreco operator +(
        TPreco a,
        TPreco b)
    {
        return new TPreco(a._value + b._value);
    }

    public static TPreco operator -(
        TPreco a,
        TPreco b)
    {
        return new TPreco(a._value - b._value);
    }

    public static TTotal operator *(
        TPreco preco,
        TQuantidade quantidade)
    {
        return new TTotal(
            preco._value * quantidade.Value);
    }

    public static TPreco operator *(
        TPreco preco,
        decimal fator)
    {
        return new TPreco(
            preco._value * fator);
    }

    public static TPreco operator *(
        decimal fator,
        TPreco preco)
    {
        return preco * fator;
    }

    public static TPreco operator /(
        TPreco preco,
        decimal divisor)
    {
        if (divisor == 0)
            throw new DivideByZeroException();

        return new TPreco(
            preco._value / divisor);
    }

    public static bool operator >(
        TPreco a,
        TPreco b)
        => a._value > b._value;

    public static bool operator <(
        TPreco a,
        TPreco b)
        => a._value < b._value;

    public static bool operator >=(
        TPreco a,
        TPreco b)
        => a._value >= b._value;

    public static bool operator <=(
        TPreco a,
        TPreco b)
        => a._value <= b._value;

    public override string ToString()
    {
        return _value.ToString("F6");
    }
}