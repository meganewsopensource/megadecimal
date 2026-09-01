namespace TMegaDecimal;

public sealed class TQuantidade : TDecimal
{
    public const int Precision = 4;

    public TQuantidade(decimal value)
        : base(value, Precision)
    {
    }

    public static TQuantidade Zero => new(0m);

    public TQuantidade Round()
    {
        return new TQuantidade(_value);
    }

    public static implicit operator TQuantidade(decimal value)
        => new(value);

    public static explicit operator decimal(TQuantidade value)
        => value._value;

    public static TQuantidade operator +(
        TQuantidade a,
        TQuantidade b)
    {
        return new TQuantidade(a._value + b._value);
    }

    public static TQuantidade operator -(
        TQuantidade a,
        TQuantidade b)
    {
        return new TQuantidade(a._value - b._value);
    }
    
    public static TQuantidade operator *(
        TQuantidade quantidade,
        decimal fator)
    {
        return new TQuantidade(
            quantidade._value * fator);
    }

    public static TQuantidade operator *(
        decimal fator,
        TQuantidade quantidade)
    {
        return quantidade * fator;
    }

    public static TQuantidade operator /(
        TQuantidade quantidade,
        decimal divisor)
    {
        if (divisor == 0)
            throw new DivideByZeroException();

        return new TQuantidade(
            quantidade._value / divisor);
    }

    public static TTotal operator *(
        TQuantidade quantidade,
        TPreco preco)
    {
        return new TTotal(
            quantidade._value * preco.Value);
    }

    public static bool operator >(
        TQuantidade a,
        TQuantidade b)
        => a._value > b._value;

    public static bool operator <(
        TQuantidade a,
        TQuantidade b)
        => a._value < b._value;

    public static bool operator >=(
        TQuantidade a,
        TQuantidade b)
        => a._value >= b._value;

    public static bool operator <=(
        TQuantidade a,
        TQuantidade b)
        => a._value <= b._value;

    public override string ToString()
    {
        return _value.ToString("F4");
    }
}