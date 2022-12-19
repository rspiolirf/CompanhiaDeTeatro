using CompanhiaDeTeatro.CasosDeUso.Excecoes;
using CompanhiaDeTeatro.CasosDeUso.Funcionalidades.GeraRecibo;
using CompanhiaDeTeatro.CasosDeUso.Interfaces;
using CompanhiaDeTeatro.Dominio.Entidades;
using CompanhiaDeTeatro.Dominio.Enumeracoes;

namespace CompanhiaDeTeatro.Tests;

public class GeraReciboTest
{
    [Fact]
    public async Task ReciboBigCo()
    {
        var handler = new GeraReciboHandler(new PecaRepositorioFake(), new FaturaRepositorioFake());

        var resultado = await handler.Handle(new GeraReciboRequest(), new CancellationToken());

        Assert.Contains("Recibo para BigCo", resultado);
        Assert.Contains("Hamlet: $65000 (55 assentos)", resultado);
        Assert.Contains("Othello: $50000 (40 assentos)", resultado);
        Assert.Contains("Total devido é $173000", resultado);
        Assert.Contains("Você recebeu 47 créditos", resultado);
    }
}

public class PecaRepositorioFake : IPecaRepositorio
{
    private readonly List<Peca> pecas = new()
    {
        new Peca() { CodigoPeca = 1, Descricao = "Hamlet", Genero = GeneroPeca.Tragedia },
        new Peca() { CodigoPeca = 2, Descricao = "As You Like It", Genero = GeneroPeca.Comedia },
        new Peca() { CodigoPeca = 3, Descricao = "Othello", Genero = GeneroPeca.Tragedia },
    };

    public Peca ConsultaPorCodigo(int codigo)
    {
        var resultado = pecas.FirstOrDefault(p => p.CodigoPeca == codigo);

        if (resultado is null)
            throw new PecaNaoEncontradaException();

        return resultado;
    }

    public IEnumerable<Peca> ConsultaTodas()
    {
        return pecas;
    }
}

public class FaturaRepositorioFake : IFaturaRepositorio
{
    public Fatura ConsultaPorCodigo(int codigo)
    {
        return new Fatura()
        {
            Codigo = codigo,
            Cliente = "BigCo",
            Apresentacoes = new List<Apresentacao>()
            {
                new Apresentacao() { CodigoPeca = 1, Publico = 55 },
                new Apresentacao() { CodigoPeca = 2, Publico = 35 },
                new Apresentacao() { CodigoPeca = 3, Publico = 40 }
            }
        };
    }
}