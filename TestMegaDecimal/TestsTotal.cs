using TMegaDecimal;
using Xunit;

namespace TestMegaMoney;

public class TestsTotal
{
    [Fact]
    public void Precision_DeveSer_Dois()
    {
        Assert.Equal(2, TTotal.Precision);
    }

    [Fact]
    public void Construtor_DeveCriarTotal()
    {
        var total = new TTotal(123.45m);

        Assert.Equal(123.45m, total.ToDecimal());
    }

    [Fact]
    public void Zero_DeveRetornarTotalZero()
    {
        var total = TTotal.Zero;

        Assert.Equal(0m, total.ToDecimal());
    }

    [Fact]
    public void ConversaoImplicita_DeveConverterDecimalParaTotal()
    {
        TTotal total = new(123.45m);

        Assert.Equal(123.45m, total.ToDecimal());
    }

    [Fact]
    public void ConversaoExplicita_DeveConverterTotalParaDecimal()
    {
        var total = new TTotal(123.45m);

        decimal resultado = total.ToDecimal();

        Assert.Equal(123.45m, resultado);
    }

    [Theory]
    [InlineData(10, 5, 15)]
    [InlineData(10.50, 2.25, 12.75)]
    [InlineData(100, 25, 125)]
    [InlineData(0, 10, 10)]
    public void Soma_DeveRetornarSomaDosTotais(
        decimal a,
        decimal b,
        decimal esperado)
    {
        var totalA = new TTotal(a);
        var totalB = new TTotal(b);

        var resultado = totalA + totalB;

        Assert.Equal(esperado, resultado.ToDecimal());
    }

    [Theory]
    [InlineData(10, 5, 5)]
    [InlineData(10.50, 2.25, 8.25)]
    [InlineData(100, 25, 75)]
    [InlineData(10, 20, -10)]
    public void Subtracao_DeveRetornarDiferencaDosTotais(
        decimal a,
        decimal b,
        decimal esperado)
    {
        var totalA = new TTotal(a);
        var totalB = new TTotal(b);

        var resultado = totalA - totalB;

        Assert.Equal(esperado, resultado.ToDecimal());
    }

    [Theory]
    [InlineData(10, 2, 20)]
    [InlineData(10.50, 2, 21)]
    [InlineData(25, 0.5, 12.5)]
    [InlineData(100, 1.5, 150)]
    public void MultiplicacaoPorDecimal_DeveRetornarTotalMultiplicado(
        decimal total,
        decimal fator,
        decimal esperado)
    {
        var value = new TTotal(total);

        var resultado = value * fator;

        Assert.Equal(esperado, resultado.ToDecimal());
    }

    [Theory]
    [InlineData(2, 10, 20)]
    [InlineData(2, 10.50, 21)]
    [InlineData(0.5, 25, 12.5)]
    [InlineData(1.5, 100, 150)]
    public void MultiplicacaoDecimalPorTotal_DeveRetornarTotalMultiplicado(
        decimal fator,
        decimal total,
        decimal esperado)
    {
        var value = new TTotal(total);

        var resultado = fator * value;

        Assert.Equal(esperado, resultado.ToDecimal());
    }

    [Theory]
    [InlineData(10, 2, 5)]
    [InlineData(100, 4, 25)]
    [InlineData(10.50, 2, 5.25)]
    [InlineData(12.50, 5, 2.5)]
    public void DivisaoPorDecimal_DeveRetornarTotalDividido(
        decimal total,
        decimal divisor,
        decimal esperado)
    {
        var value = new TTotal(total);

        var resultado = value / divisor;

        Assert.Equal(esperado, resultado.ToDecimal());
    }

    [Fact]
    public void DivisaoPorDecimalZero_DeveLancarDivideByZeroException()
    {
        var total = new TTotal(10m);

        Assert.Throws<DivideByZeroException>(() =>
            total / 0m);
    }

    [Theory]
    [InlineData(100, 2, 50)]
    [InlineData(250, 5, 50)]
    [InlineData(123.45, 3, 41.15)]
    public void DivisaoPorQuantidade_DeveRetornarPreco(
        decimal total,
        decimal quantidade,
        decimal esperado)
    {
        var value = new TTotal(total);
        var qtd = new TQuantidade(quantidade);

        TPreco resultado = value / qtd;

        Assert.Equal(esperado, (decimal)resultado);
    }

    [Fact]
    public void DivisaoPorQuantidadeZero_DeveLancarDivideByZeroException()
    {
        var total = new TTotal(100m);
        var quantidade = new TQuantidade(0m);

        Assert.Throws<DivideByZeroException>(() =>
            total / quantidade);
    }

    [Theory]
    [InlineData(100, 10, 10)]
    [InlineData(250, 5, 50)]
    [InlineData(123.45, 3, 41.15)]
    public void DivisaoPorPreco_DeveRetornarQuantidade(
        decimal total,
        decimal preco,
        decimal esperado)
    {
        var value = new TTotal(total);
        var price = new TPreco(preco);

        TQuantidade resultado = value / price;

        Assert.Equal(esperado, (decimal)resultado);
    }

    [Fact]
    public void DivisaoPorPrecoZero_DeveLancarDivideByZeroException()
    {
        var total = new TTotal(100m);
        var preco = new TPreco(0m);

        Assert.Throws<DivideByZeroException>(() =>
            total / preco);
    }

    [Theory]
    [InlineData(100, 10, 10)]
    [InlineData(250, 20, 50)]
    [InlineData(123.45, 10, 12.35)]
    public void MultiplicacaoPorPercentual_DeveCalcularPercentual(
        decimal total,
        decimal percentual,
        decimal esperado)
    {
        var value = new TTotal(total);
        var percent = new TPercentual(percentual);

        var resultado = value * percent;

        Assert.Equal(esperado, resultado.ToDecimal());
    }

    [Theory]
    [InlineData(100, 10, 10)]
    [InlineData(250, 20, 50)]
    [InlineData(123.45, 10, 12.35)]
    public void MultiplicacaoPercentualPorTotal_DeveCalcularPercentual(
        decimal percentual,
        decimal total,
        decimal esperado)
    {
        var percent = new TPercentual(percentual);
        var value = new TTotal(total);

        var resultado = percent * value;

        Assert.Equal(esperado, resultado.ToDecimal());
    }

    [Theory]
    [InlineData(100, 10, 90)]
    [InlineData(100, 25, 75)]
    [InlineData(250, 20, 200)]
    [InlineData(123.45, 10, 111.11)]
    public void AplicarDesconto_DeveRetornarTotalComDesconto(
        decimal total,
        decimal percentual,
        decimal esperado)
    {
        var value = new TTotal(total);
        var percent = new TPercentual(percentual);

        var resultado = value.AplicarDesconto(percent);

        Assert.Equal(esperado, resultado.ToDecimal());
    }

    [Theory]
    [InlineData(100, 10, 110)]
    [InlineData(100, 25, 125)]
    [InlineData(250, 20, 300)]
    [InlineData(123.45, 10, 135.80)]
    public void AplicarAcrescimo_DeveRetornarTotalComAcrescimo(
        decimal total,
        decimal percentual,
        decimal esperado)
    {
        var value = new TTotal(total);
        var percent = new TPercentual(percentual);

        var resultado = value.AplicarAcrescimo(percent);

        Assert.Equal(esperado, resultado.ToDecimal());
    }

    [Theory]
    [InlineData(100, 10, 10)]
    [InlineData(100, 25, 25)]
    [InlineData(250, 20, 50)]
    [InlineData(123.45, 10, 12.35)]
    public void CalcularPercentual_DeveRetornarValorDoPercentual(
        decimal total,
        decimal percentual,
        decimal esperado)
    {
        var value = new TTotal(total);
        var percent = new TPercentual(percentual);

        var resultado = value.CalcularPercentual(percent);

        Assert.Equal(esperado, resultado.ToDecimal());
    }

    [Theory]
    [InlineData(10, 5, true)]
    [InlineData(5, 10, false)]
    [InlineData(5, 5, false)]
    public void MaiorQue_DeveCompararTotais(
        decimal a,
        decimal b,
        bool esperado)
    {
        var resultado =
            new TTotal(a) > new TTotal(b);

        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(5, 10, true)]
    [InlineData(10, 5, false)]
    [InlineData(5, 5, false)]
    public void MenorQue_DeveCompararTotais(
        decimal a,
        decimal b,
        bool esperado)
    {
        var resultado =
            new TTotal(a) < new TTotal(b);

        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(10, 5, true)]
    [InlineData(5, 10, false)]
    [InlineData(5, 5, true)]
    public void MaiorOuIgual_DeveCompararTotais(
        decimal a,
        decimal b,
        bool esperado)
    {
        var resultado =
            new TTotal(a) >= new TTotal(b);

        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(5, 10, true)]
    [InlineData(10, 5, false)]
    [InlineData(5, 5, true)]
    public void MenorOuIgual_DeveCompararTotais(
        decimal a,
        decimal b,
        bool esperado)
    {
        var resultado =
            new TTotal(a) <= new TTotal(b);

        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(12.5)]
    [InlineData(99.99)]
    [InlineData(0)]
    [InlineData(-10.25)]
    public void ToString_DeveFormatarTotalComDuasCasas(decimal value)
    {
        // Esperado calculado com a cultura da maquina atual, para que o
        // teste passe independentemente do separador decimal do ambiente.
        var esperado = value.ToString("F2");

        var total = new TTotal(value);

        var resultado = total.ToString();

        Assert.Equal(esperado, resultado);
    }
}