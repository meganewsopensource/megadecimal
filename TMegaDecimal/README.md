# TMegaDecimal

Biblioteca .NET para trabalhar com **valores monetários e decimais fortemente tipados**, reduzindo erros de cálculo causados pelo uso indiscriminado de `decimal` e deixando as operações do domínio mais explícitas.

A biblioteca foi desenvolvida para aplicações comerciais, financeiras e de gestão que precisam trabalhar com valores decimais de forma previsível e com precisão definida para cada tipo de informação.

## Características

* Compatível com **.NET 10**
* Baseada no tipo `decimal` do .NET
* Precisão definida por tipo
* Tipos específicos para diferentes conceitos de negócio
* Operadores matemáticos fortemente tipados
* Validação de operações inválidas
* Evita misturar acidentalmente valores de naturezas diferentes
* Pode ser distribuída como pacote NuGet

## Instalação

Instale o pacote através do NuGet:

```bash
dotnet add package TMegaDecimal
```

Ou através do Package Manager Console:

```powershell
Install-Package TMegaDecimal
```

Também é possível instalar diretamente pelo gerenciador de pacotes NuGet do Visual Studio.

## Tipos disponíveis

A biblioteca utiliza tipos específicos para representar diferentes conceitos numéricos.

| Tipo          | Utilização                    |           Precisão |
| ------------- | ----------------------------- | -----------------: |
| `TDecimal`    | Tipo decimal base             | Definida pelo tipo |
| `TTotal`      | Valores monetários totais     |            2 casas |
| `TPreco`      | Preços unitários              |            2 casas |
| `TQuantidade` | Quantidades de produtos/itens |            6 casas |
| `TPercentual` | Percentuais                   |            2 casas |

> A precisão de cada tipo é uma regra de domínio. Ela pode ser alterada conforme as necessidades da aplicação.

## Por que utilizar tipos específicos?

Uma aplicação comercial frequentemente possui diversos valores `decimal`:

```csharp
decimal total;
decimal preco;
decimal quantidade;
decimal percentual;
```

Embora todos sejam `decimal`, eles representam conceitos completamente diferentes.

Isso permite, por exemplo, que um preço seja utilizado acidentalmente onde uma quantidade era esperada.

Com `TMegaDecimal`, esses conceitos podem ser representados explicitamente:

```csharp
TTotal total;
TPreco preco;
TQuantidade quantidade;
TPercentual percentual;
```

Além de tornar o código mais expressivo, isso permite que o compilador ajude a identificar operações que não fazem sentido no domínio.

## Exemplos

### Criando valores

```csharp
var total = new TTotal(150.50m);
var preco = new TPreco(25.90m);
var quantidade = new TQuantidade(6);
var percentual = new TPercentual(10);
```

### Soma de totais

```csharp
var a = new TTotal(100.00m);
var b = new TTotal(50.50m);

TTotal resultado = a + b;
```

Resultado:

```text
150,50
```

### Subtração

```csharp
var total = new TTotal(150.00m);
var desconto = new TTotal(20.00m);

TTotal resultado = total - desconto;
```

Resultado:

```text
130,00
```

### Multiplicação por quantidade

Uma operação comum em aplicações comerciais é calcular o total de um item:

```csharp
var preco = new TPreco(25.50m);
var quantidade = new TQuantidade(4);

TTotal total = preco * quantidade;
```

Resultado:

```text
102,00
```

A operação deixa explícito que estamos multiplicando um **preço** por uma **quantidade** para obter um **total**.

### Percentual

```csharp
var total = new TTotal(1000.00m);
var percentual = new TPercentual(10.00m);
```

Um cálculo de percentual pode produzir um valor monetário de acordo com as operações disponibilizadas pela biblioteca.

## Precisão

Os valores são armazenados internamente utilizando `decimal`, evitando os problemas de representação binária encontrados em tipos como `double` e `float`.

Por exemplo:

```csharp
decimal valor = 0.1m + 0.2m;
```

O uso do sufixo `m` garante que o literal seja tratado como `decimal`.

A biblioteca utiliza `decimal` como base para operações financeiras e aplica a precisão definida para cada tipo.

## Arredondamento

Cada tipo possui uma precisão definida de acordo com seu propósito.

Por exemplo:

```csharp
public sealed class TTotal : TDecimal
{
    public const int Precision = 2;

    public TTotal(decimal value)
        : base(value, Precision)
    {
    }
}
```

Assim, valores monetários podem ser normalizados para duas casas decimais.

Isso evita que diferentes partes da aplicação adotem regras de arredondamento diferentes.

## Segurança de tipos

Um dos principais objetivos da biblioteca é impedir operações semanticamente incorretas.

Por exemplo, um `TPreco` representa um preço, enquanto `TQuantidade` representa uma quantidade.

Em vez de trabalhar com:

```csharp
decimal preco;
decimal quantidade;
decimal total;
```

podemos trabalhar com:

```csharp
TPreco preco;
TQuantidade quantidade;
TTotal total;
```

Isso torna a intenção do código muito mais clara.

## Valores zero

Os tipos que representam valores monetários podem disponibilizar um valor `Zero`:

```csharp
var total = TTotal.Zero;
```

Isso evita a necessidade de utilizar diretamente:

```csharp
new TTotal(0m);
```

e torna o código mais expressivo.

## Banco de dados

A biblioteca foi projetada para trabalhar com valores `decimal` e pode ser utilizada em conjunto com bancos de dados que suportem tipos decimais de precisão fixa.

Para valores monetários, recomenda-se utilizar tipos equivalentes a:

```sql
DECIMAL(15,2)
```

Para quantidades que necessitam de maior precisão:

```sql
DECIMAL(15,6)
```

A precisão do banco deve ser compatível com a precisão definida no respectivo tipo da aplicação.

## Arquitetura

A estrutura básica da biblioteca é:

```text
TMegaDecimal
│
├── TDecimal
│
├── TTotal
├── TPreco
├── TQuantidade
└── TPercentual
```

`TDecimal` fornece o comportamento comum, enquanto os tipos especializados definem as regras específicas de cada conceito.

## Requisitos

* .NET 10

## Versionamento

A biblioteca utiliza versionamento semântico:

```text
MAJOR.MINOR.PATCH
```

Exemplos:

```text
1.0.0
1.0.1
1.1.0
2.0.0
```

### PATCH

Correções que não alteram a API ou o comportamento esperado de forma incompatível.

```text
1.0.0 → 1.0.1
```

### MINOR

Novas funcionalidades compatíveis com versões anteriores.

```text
1.0.0 → 1.1.0
```

### MAJOR

Alterações incompatíveis com versões anteriores.

```text
1.0.0 → 2.0.0
```

## Desenvolvimento

Clone o repositório:

```bash
git clone https://github.com/suaempresa/TMegaDecimal.git
```

Entre no diretório:

```bash
cd TMegaDecimal
```

Restaure as dependências:

```bash
dotnet restore
```

Compile:

```bash
dotnet build
```

Execute os testes:

```bash
dotnet test
```

## Gerando o pacote NuGet

Para gerar o pacote localmente:

```bash
dotnet pack -c Release
```

O pacote será gerado em:

```text
bin/Release/
```

Por exemplo:

```text
TMegaDecimal.1.0.0.nupkg
```

## Publicação

As versões oficiais do pacote são publicadas automaticamente através de **GitHub Actions**.

A publicação é acionada pela criação de uma tag de versão:

```bash
git tag v1.0.0
git push origin v1.0.0
```

O processo de CI executa:

```text
Build
  ↓
Test
  ↓
Pack
  ↓
Publish
  ↓
NuGet
```

Uma versão só é publicada caso a compilação e os testes sejam concluídos com sucesso.

## Licença

Este projeto é distribuído sob a licença definida no arquivo `LICENSE`.

---

**TMegaDecimal** — tipos fortes para valores decimais de domínio financeiro.
