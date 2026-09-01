using TMegaDecimal;
using Xunit;

namespace TestMegaMoney;

public class TestsQuantidade
{
    [Fact]
    public void Precision_DeveSer_Quatro()
    {
        Assert.Equal(4, TQuantidade.Precision);
    }

    [Fact]
    public void Construtor_DeveCriarQuantidade()
    {
        var quantidade = new TQuantidade(12.3456m);

        Assert.Equal(12.3456m, (decimal)quantidade);
    }

    [Fact]
    public void Zero_DeveRetornarQuantidadeZero()
    {
        var quantidade = TQuantidade.Zero;

        Assert.Equal(0m, (decimal)quantidade);
    }

    [Fact]
    public void Round_DeveRetornarQuantidade()
    {
        var quantidade = new TQuantidade(12.3456m);

        var resultado = quantidade.Round();

        Assert.Equal(12.3456m, (decimal)resultado);
    }

    [Fact]
    public void ConversaoImplicita_DeveConverterDecimalParaQuantidade()
    {
        TQuantidade quantidade = 12.3456m;

        Assert.Equal(12.3456m, (decimal)quantidade);
    }

    [Fact]
    public void ConversaoExplicita_DeveConverterQuantidadeParaDecimal()
    {
        var quantidade = new TQuantidade(12.3456m);

        decimal resultado = (decimal)quantidade;

        Assert.Equal(12.3456m, resultado);
    }

    [Theory]
    [InlineData(10, 5, 15)]
    [InlineData(10.5, 2.25, 12.75)]
    [InlineData(100, 25, 125)]
    [InlineData(0, 10, 10)]
    public void Soma_DeveRetornarSomaDasQuantidades(
        decimal a,
        decimal b,
        decimal esperado)
    {
        var quantidadeA = new TQuantidade(a);
        var quantidadeB = new TQuantidade(b);

        var resultado = quantidadeA + quantidadeB;

        Assert.Equal(esperado, (decimal)resultado);
    }

    [Theory]
    [InlineData(10, 5, 5)]
    [InlineData(10.5, 2.25, 8.25)]
    [InlineData(100, 25, 75)]
    [InlineData(10, 20, -10)]
    public void Subtracao_DeveRetornarDiferencaDasQuantidades(
        decimal a,
        decimal b,
        decimal esperado)
    {
        var quantidadeA = new TQuantidade(a);
        var quantidadeB = new TQuantidade(b);

        var resultado = quantidadeA - quantidadeB;

        Assert.Equal(esperado, (decimal)resultado);
    }

    [Theory]
    [InlineData(10, 2, 20)]
    [InlineData(10.5, 2, 21)]
    [InlineData(25, 0.5, 12.5)]
    [InlineData(100, 1.5, 150)]
    public void MultiplicacaoPorDecimal_DeveRetornarQuantidadeMultiplicada(
        decimal quantidade,
        decimal fator,
        decimal esperado)
    {
        var value = new TQuantidade(quantidade);

        var resultado = value * fator;

        Assert.Equal(esperado, (decimal)resultado);
    }

    [Theory]
    [InlineData(10, 2, 20)]
    [InlineData(10.5, 2, 21)]
    [InlineData(25, 0.5, 12.5)]
    [InlineData(100, 1.5, 150)]
    public void MultiplicacaoDecimalPorQuantidade_DeveRetornarQuantidadeMultiplicada(
        decimal fator,
        decimal quantidade,
        decimal esperado)
    {
        var value = new TQuantidade(quantidade);

        var resultado = fator * value;

        Assert.Equal(esperado, (decimal)resultado);
    }

    [Theory]
    [InlineData(10, 2, 5)]
    [InlineData(100, 4, 25)]
    [InlineData(10.5, 2, 5.25)]
    [InlineData(12.5, 5, 2.5)]
    public void Divisao_DeveRetornarQuantidadeDividida(
        decimal quantidade,
        decimal divisor,
        decimal esperado)
    {
        var value = new TQuantidade(quantidade);

        var resultado = value / divisor;

        Assert.Equal(esperado, (decimal)resultado);
    }

    [Fact]
    public void DivisaoPorZero_DeveLancarDivideByZeroException()
    {
        var quantidade = new TQuantidade(10m);

        Assert.Throws<DivideByZeroException>(() =>
            quantidade / 0m);
    }

    [Theory]
    [InlineData(2, 10, 20)]
    [InlineData(5, 12.5, 62.5)]
    [InlineData(10, 3.25, 32.5)]
    [InlineData(2.5, 100, 250)]
    public void MultiplicacaoPorPreco_DeveRetornarTotal(
        decimal quantidade,
        decimal preco,
        decimal esperado)
    {
        var value = new TQuantidade(quantidade);
        var price = new TPreco(preco);

        TTotal resultado = value * price;

        Assert.Equal(esperado, resultado.ToDecimal());
    }

    [Theory]
    [InlineData(10, 5, true)]
    [InlineData(5, 10, false)]
    [InlineData(5, 5, false)]
    public void MaiorQue_DeveCompararQuantidades(
        decimal a,
        decimal b,
        bool esperado)
    {
        var resultado =
            new TQuantidade(a) > new TQuantidade(b);

        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(5, 10, true)]
    [InlineData(10, 5, false)]
    [InlineData(5, 5, false)]
    public void MenorQue_DeveCompararQuantidades(
        decimal a,
        decimal b,
        bool esperado)
    {
        var resultado =
            new TQuantidade(a) < new TQuantidade(b);

        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(10, 5, true)]
    [InlineData(5, 10, false)]
    [InlineData(5, 5, true)]
    public void MaiorOuIgual_DeveCompararQuantidades(
        decimal a,
        decimal b,
        bool esperado)
    {
        var resultado =
            new TQuantidade(a) >= new TQuantidade(b);

        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(5, 10, true)]
    [InlineData(10, 5, false)]
    [InlineData(5, 5, true)]
    public void MenorOuIgual_DeveCompararQuantidades(
        decimal a,
        decimal b,
        bool esperado)
    {
        var resultado =
            new TQuantidade(a) <= new TQuantidade(b);

        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(10, "10,0000")]
    [InlineData(12.5, "12,5000")]
    [InlineData(99.1234, "99,1234")]
    [InlineData(0, "0,0000")]
    [InlineData(-10.25, "-10,2500")]
    public void ToString_DeveFormatarQuantidadeComQuatroCasas(
        decimal value,
        string esperado)
    {
        var quantidade = new TQuantidade(value);

        var resultado = quantidade.ToString();

        Assert.Equal(esperado, resultado);
    }
}