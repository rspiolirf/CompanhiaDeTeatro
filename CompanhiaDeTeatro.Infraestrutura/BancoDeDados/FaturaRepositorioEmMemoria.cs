using CompanhiaDeTeatro.CasosDeUso.Excecoes;
using CompanhiaDeTeatro.CasosDeUso.Interfaces;
using CompanhiaDeTeatro.Dominio.Entidades;

namespace CompanhiaDeTeatro.Infraestrutura.BancoDeDados;

public class FaturaRepositorioEmMemoria : IFaturaRepositorio
{
    private readonly List<Fatura> _faturas = new()
    {
        new Fatura()
        {
            Codigo = 1,
            Cliente = "BigCo",
            Apresentacoes = new List<Apresentacao>()
            {
                new Apresentacao() { CodigoPeca = 1, Publico = 55 },
                new Apresentacao() { CodigoPeca = 2, Publico = 35 },
                new Apresentacao() { CodigoPeca = 3, Publico = 40 }
            }
        },
        new Fatura()
        {
            Codigo = 2,
            Cliente = "MonsterJamCo",
            Apresentacoes = new List<Apresentacao>()
            {
                new Apresentacao() { CodigoPeca = 3, Publico = 30 },
                new Apresentacao() { CodigoPeca = 1, Publico = 18 },
                new Apresentacao() { CodigoPeca = 2, Publico = 22 }
            }
        }
    };

    public Fatura ConsultaPorCodigo(int codigo)
    {
        var resultado = _faturas.FirstOrDefault(f => f.Codigo == codigo);

        if (resultado is null)
            throw new FaturaNaoEncontradaException();

        return resultado;
    }
}
