using CompanhiaDeTeatro.Dominio.Entidades;

namespace CompanhiaDeTeatro.CasosDeUso.Interfaces;

public interface IFaturaRepositorio
{
    Fatura ConsultaPorCodigo(int codigo);
}
