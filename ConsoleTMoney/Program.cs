using TMegaDecimal;

namespace ConsoleTMoney;

class Program
{
    static void Main(string[] args)
    {
        TQuantidade quantidade = new TQuantidade(4);
        TPreco preco = new TPreco(5.671m);
        TTotal total = quantidade * preco;
        Console.WriteLine("O total de " + total);
        
        TQuantidade quantidade2 = 3.1254m;
        TPreco preco2 = 15.678901m;
        TPercentual desconto = 5.25m;

        TTotal totalBruto = quantidade2 * preco2;

        TTotal valorDesconto =
            totalBruto * desconto;

        TTotal totalLiquido =
            totalBruto - valorDesconto;

        Console.WriteLine($"Quantidade:     {quantidade2}");
        Console.WriteLine($"Preço unitário: {preco2}");
        Console.WriteLine($"Total bruto:    {totalBruto}");
        Console.WriteLine($"Desconto:       {valorDesconto}");
        Console.WriteLine($"Total líquido:  {totalLiquido}");
    }
}