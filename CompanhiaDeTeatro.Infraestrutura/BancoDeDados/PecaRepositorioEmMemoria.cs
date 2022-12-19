using CompanhiaDeTeatro.CasosDeUso.Excecoes;
using CompanhiaDeTeatro.CasosDeUso.Interfaces;
using CompanhiaDeTeatro.Dominio.Entidades;
using CompanhiaDeTeatro.Dominio.Enumeracoes;

namespace CompanhiaDeTeatro.Infraestrutura.BancoDeDados
{
    public class PecaRepositorioEmMemoria : IPecaRepositorio
    {
        private readonly List<Peca> pecas = new ()
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
}