namespace TMegaDecimal;

public sealed class TTotal : TDecimal
{
    public const int Precision = 2;

    public TTotal(decimal value)
        : base(value, Precision)
    {
    }

    public static TTotal Zero => new(0m);
    
    public static TTotal operator +(
        TTotal a,
        TTotal b)
    {
        return new TTotal(
            a._value + b._value);
    }

    public static TTotal operator -(
        TTotal a,
        TTotal b)
    {
        return new TTotal(
            a._value - b._value);
    }

    public static TTotal operator *(
        TTotal total,
        decimal fator)
    {
        return new TTotal(
            total._value * fator);
    }

    public static TTotal operator *(
        decimal fator,
        TTotal total)
    {
        return total * fator;
    }

    public static TTotal operator /(
        TTotal total,
        decimal divisor)
    {
        if (divisor == 0)
            throw new DivideByZeroException();

        return new TTotal(
            total._value / divisor);
    }
    
    public static TPreco operator /(
        TTotal total,
        TQuantidade quantidade)
    {
        if (quantidade.Value == 0)
            throw new DivideByZeroException();

        return new TPreco(
            total._value / quantidade.Value);
    }
    
    public static TQuantidade operator /(
        TTotal total,
        TPreco preco)
    {
        if (preco.Value == 0)
            throw new DivideByZeroException();

        return new TQuantidade(
            total.Value / preco.Value);
    }

    public static TTotal operator *(
        TTotal total,
        TPercentual percentual)
    {
        return new TTotal(
            total._value * percentual.ToFactor());
    }

    public static TTotal operator *(
        TPercentual percentual,
        TTotal total)
    {
        return total * percentual;
    }

    public TTotal AplicarDesconto(
        TPercentual percentual)
    {
        return new TTotal(
            _value *
            (1m - percentual.ToFactor()));
    }

    public TTotal AplicarAcrescimo(
        TPercentual percentual)
    {
        return new TTotal(
            _value *
            (1m + percentual.ToFactor()));
    }

    public TTotal CalcularPercentual(
        TPercentual percentual)
    {
        return new TTotal(
            _value *
            percentual.ToFactor());
    }

    public static bool operator >(
        TTotal a,
        TTotal b)
        => a._value > b._value;

    public static bool operator <(
        TTotal a,
        TTotal b)
        => a._value < b._value;

    public static bool operator >=(
        TTotal a,
        TTotal b)
        => a._value >= b._value;

    public static bool operator <=(
        TTotal a,
        TTotal b)
        => a._value <= b._value;

    public override string ToString()
    {
        return _value.ToString("F2");
    }
}