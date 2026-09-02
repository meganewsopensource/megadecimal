using TMegaDecimal;
using Xunit;

namespace TestMegaMoney;

public class TestsPreco
{
    [Fact]
    public void Precision_DeveSer_Seis()
    {
        Assert.Equal(6, TPreco.Precision);
    }

    [Fact]
    public void Construtor_DeveCriarPreco()
    {
        var preco = new TPreco(12.345678m);

        Assert.Equal(12.345678m, (decimal)preco);
    }

    [Fact]
    public void Zero_DeveRetornarPrecoZero()
    {
        var preco = TPreco.Zero;

        Assert.Equal(0m, (decimal)preco);
    }

    [Fact]
    public void ConversaoImplicita_DeveConverterDecimalParaPreco()
    {
        TPreco preco = 12.345678m;

        Assert.Equal(12.345678m, (decimal)preco);
    }

    [Fact]
    public void ConversaoExplicita_DeveConverterPrecoParaDecimal()
    {
        var preco = new TPreco(12.345678m);

        decimal resultado = (decimal)preco;

        Assert.Equal(12.345678m, resultado);
    }

    [Theory]
    [InlineData(10, 5, 15)]
    [InlineData(10.5, 2.25, 12.75)]
    [InlineData(100, 25, 125)]
    [InlineData(0, 10, 10)]
    public void Soma_DeveRetornarSomaDosPrecos(
        decimal a,
        decimal b,
        decimal esperado)
    {
        var precoA = new TPreco(a);
        var precoB = new TPreco(b);

        var resultado = precoA + precoB;

        Assert.Equal(esperado, (decimal)resultado);
    }

    [Theory]
    [InlineData(10, 5, 5)]
    [InlineData(10.5, 2.25, 8.25)]
    [InlineData(100, 25, 75)]
    [InlineData(10, 20, -10)]
    public void Subtracao_DeveRetornarDiferencaDosPrecos(
        decimal a,
        decimal b,
        decimal esperado)
    {
        var precoA = new TPreco(a);
        var precoB = new TPreco(b);

        var resultado = precoA - precoB;

        Assert.Equal(esperado, (decimal)resultado);
    }

    [Theory]
    [InlineData(10, 2, 20)]
    [InlineData(10.5, 2, 21)]
    [InlineData(25, 0.5, 12.5)]
    [InlineData(100, 1.5, 150)]
    public void MultiplicacaoPorDecimal_DeveRetornarPrecoMultiplicado(
        decimal preco,
        decimal fator,
        decimal esperado)
    {
        var value = new TPreco(preco);

        var resultado = value * fator;

        Assert.Equal(esperado, (decimal)resultado);
    }

    [Theory]
    [InlineData(10, 2, 20)]
    [InlineData(10.5, 2, 21)]
    [InlineData(25, 0.5, 12.5)]
    [InlineData(100, 1.5, 150)]
    public void MultiplicacaoDecimalPorPreco_DeveRetornarPrecoMultiplicado(
        decimal preco,
        decimal fator,
        decimal esperado)
    {
        var value = new TPreco(preco);

        var resultado = fator * value;

        Assert.Equal(esperado, (decimal)resultado);
    }

    [Theory]
    [InlineData(10, 2, 20)]
    [InlineData(10.5, 2, 21)]
    [InlineData(100, 4, 400)]
    [InlineData(12.5, 3, 37.5)]
    public void MultiplicacaoPorQuantidade_DeveRetornarTotal(
        decimal preco,
        decimal quantidade,
        decimal esperado)
    {
        var value = new TPreco(preco);
        var qtd = new TQuantidade(quantidade);

        TTotal resultado = value * qtd;

        Assert.Equal(esperado, resultado.ToDecimal());
    }

    [Theory]
    [InlineData(10, 2, 5)]
    [InlineData(100, 4, 25)]
    [InlineData(10.5, 2, 5.25)]
    [InlineData(12.5, 5, 2.5)]
    public void Divisao_DeveRetornarPrecoDividido(
        decimal preco,
        decimal divisor,
        decimal esperado)
    {
        var value = new TPreco(preco);

        var resultado = value / divisor;

        Assert.Equal(esperado, (decimal)resultado);
    }

    [Fact]
    public void DivisaoPorZero_DeveLancarDivideByZeroException()
    {
        var preco = new TPreco(10m);

        Assert.Throws<DivideByZeroException>(() =>
            preco / 0m);
    }

    [Theory]
    [InlineData(10, 5, true)]
    [InlineData(5, 10, false)]
    [InlineData(5, 5, false)]
    public void MaiorQue_DeveCompararPrecos(
        decimal a,
        decimal b,
        bool esperado)
    {
        var resultado =
            new TPreco(a) > new TPreco(b);

        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(5, 10, true)]
    [InlineData(10, 5, false)]
    [InlineData(5, 5, false)]
    public void MenorQue_DeveCompararPrecos(
        decimal a,
        decimal b,
        bool esperado)
    {
        var resultado =
            new TPreco(a) < new TPreco(b);

        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(10, 5, true)]
    [InlineData(5, 10, false)]
    [InlineData(5, 5, true)]
    public void MaiorOuIgual_DeveCompararPrecos(
        decimal a,
        decimal b,
        bool esperado)
    {
        var resultado =
            new TPreco(a) >= new TPreco(b);

        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(5, 10, true)]
    [InlineData(10, 5, false)]
    [InlineData(5, 5, true)]
    public void MenorOuIgual_DeveCompararPrecos(
        decimal a,
        decimal b,
        bool esperado)
    {
        var resultado =
            new TPreco(a) <= new TPreco(b);

        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(12.5)]
    [InlineData(99.123456)]
    [InlineData(0)]
    [InlineData(-10.25)]
    public void ToString_DeveFormatarPrecoComSeisCasas(decimal value)
    {
        // Esperado calculado com a cultura da maquina atual, para que o
        // teste passe independentemente do separador decimal do ambiente.
        var esperado = value.ToString("F6");

        var preco = new TPreco(value);

        var resultado = preco.ToString();

        Assert.Equal(esperado, resultado);
    }
}