using MediatR;

namespace CompanhiaDeTeatro.CasosDeUso.Funcionalidades.GeraRecibo;

public class GeraReciboRequest : IRequest<string>
{
    public int CodigoRecibo { get; set; }
}