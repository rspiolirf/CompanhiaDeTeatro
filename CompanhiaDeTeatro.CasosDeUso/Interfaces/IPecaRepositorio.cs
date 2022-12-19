using CompanhiaDeTeatro.Dominio.Entidades;

namespace CompanhiaDeTeatro.CasosDeUso.Interfaces;

public interface IPecaRepositorio
{
    IEnumerable<Peca> ConsultaTodas();

    Peca ConsultaPorCodigo(int codigo);
}
