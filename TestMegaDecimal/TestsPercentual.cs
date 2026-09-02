using TMegaDecimal;
using Xunit;

namespace TestMegaMoney;

public class TestsPercentual
{
    [Fact]
    public void Precision_DeveSer_Quatro()
    {
        Assert.Equal(4, TPercentual.Precision);
    }

    [Fact]
    public void Construtor_DeveCriarPercentual()
    {
        var percentual = new TPercentual(12.3456m);

        Assert.Equal(12.3456m, (decimal)percentual);
    }

    [Fact]
    public void Zero_DeveRetornarPercentualZero()
    {
        var percentual = TPercentual.Zero;

        Assert.Equal(0m, (decimal)percentual);
    }

    [Theory]
    [InlineData(10, 0.1)]
    [InlineData(25, 0.25)]
    [InlineData(50, 0.5)]
    [InlineData(100, 1)]
    [InlineData(12.5, 0.125)]
    public void ToFactor_DeveConverterPercentualParaFator(
        decimal percentual,
        decimal esperado)
    {
        var value = new TPercentual(percentual);

        var resultado = value.ToFactor();

        Assert.Equal(esperado, resultado);
    }

    [Fact]
    public void ConversaoImplicita_DeveConverterDecimalParaPercentual()
    {
        TPercentual percentual = 15.25m;

        Assert.Equal(15.25m, (decimal)percentual);
    }

    [Fact]
    public void ConversaoExplicita_DeveConverterPercentualParaDecimal()
    {
        var percentual = new TPercentual(15.25m);

        decimal resultado = (decimal)percentual;

        Assert.Equal(15.25m, resultado);
    }

    [Theory]
    [InlineData(10, 5, 15)]
    [InlineData(10.5, 2.25, 12.75)]
    [InlineData(100, 25, 125)]
    public void Soma_DeveRetornarSomaDosPercentuais(
        decimal a,
        decimal b,
        decimal esperado)
    {
        var percentualA = new TPercentual(a);
        var percentualB = new TPercentual(b);

        var resultado = percentualA + percentualB;

        Assert.Equal(esperado, (decimal)resultado);
    }

    [Theory]
    [InlineData(10, 5, 5)]
    [InlineData(10.5, 2.25, 8.25)]
    [InlineData(100, 25, 75)]
    public void Subtracao_DeveRetornarDiferencaDosPercentuais(
        decimal a,
        decimal b,
        decimal esperado)
    {
        var percentualA = new TPercentual(a);
        var percentualB = new TPercentual(b);

        var resultado = percentualA - percentualB;

        Assert.Equal(esperado, (decimal)resultado);
    }

    [Theory]
    [InlineData(10, 2, 20)]
    [InlineData(10.5, 2, 21)]
    [InlineData(25, 0.5, 12.5)]
    [InlineData(100, 1.5, 150)]
    public void Multiplicacao_DeveMultiplicarPercentualPorFator(
        decimal percentual,
        decimal fator,
        decimal esperado)
    {
        var value = new TPercentual(percentual);

        var resultado = value * fator;

        Assert.Equal(esperado, (decimal)resultado);
    }

    [Theory]
    [InlineData(10, 5, true)]
    [InlineData(5, 10, false)]
    [InlineData(5, 5, false)]
    public void MaiorQue_DeveCompararPercentuais(
        decimal a,
        decimal b,
        bool esperado)
    {
        var resultado =
            new TPercentual(a) > new TPercentual(b);

        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(5, 10, true)]
    [InlineData(10, 5, false)]
    [InlineData(5, 5, false)]
    public void MenorQue_DeveCompararPercentuais(
        decimal a,
        decimal b,
        bool esperado)
    {
        var resultado =
            new TPercentual(a) < new TPercentual(b);

        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(10, 5, true)]
    [InlineData(5, 10, false)]
    [InlineData(5, 5, true)]
    public void MaiorOuIgual_DeveCompararPercentuais(
        decimal a,
        decimal b,
        bool esperado)
    {
        var resultado =
            new TPercentual(a) >= new TPercentual(b);

        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(5, 10, true)]
    [InlineData(10, 5, false)]
    [InlineData(5, 5, true)]
    public void MenorOuIgual_DeveCompararPercentuais(
        decimal a,
        decimal b,
        bool esperado)
    {
        var resultado =
            new TPercentual(a) <= new TPercentual(b);

        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(12.5)]
    [InlineData(99.1234)]
    [InlineData(0)]
    public void ToString_DeveFormatarPercentualComQuatroCasas(decimal value)
    {
        // Esperado calculado com a cultura da maquina atual, para que o
        // teste passe independentemente do separador decimal do ambiente.
        var esperado = value.ToString("F4") + "%";

        var percentual = new TPercentual(value);

        var resultado = percentual.ToString();

        Assert.Equal(esperado, resultado);
    }
}