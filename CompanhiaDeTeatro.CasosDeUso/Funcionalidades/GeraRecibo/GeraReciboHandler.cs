using CompanhiaDeTeatro.CasosDeUso.Interfaces;
using CompanhiaDeTeatro.Dominio.Enumeracoes;
using MediatR;

namespace CompanhiaDeTeatro.CasosDeUso.Funcionalidades.GeraRecibo;

public class GeraReciboHandler : IRequestHandler<GeraReciboRequest, string>
{
    private readonly IPecaRepositorio _pecas;
    private readonly IFaturaRepositorio _faturas;

    public GeraReciboHandler(IPecaRepositorio pecas, IFaturaRepositorio faturas)
    {
        _pecas = pecas;
        _faturas = faturas;
    }

    public async Task<string> Handle(GeraReciboRequest request, CancellationToken cancellationToken)
    {
        var bigCoFatura = _faturas.ConsultaPorCodigo(request.CodigoRecibo);
        var pecas = _pecas.ConsultaTodas();

        decimal valorTotal = 0;
        int creditorPorVolume = 0;
        var resultado = $"Recibo para {bigCoFatura.Cliente}\r\n";

        foreach (var apresentacao in bigCoFatura.Apresentacoes)
        {
            var peca = pecas.First(p => p.CodigoPeca == apresentacao.CodigoPeca);
            decimal totalPeca = 0;

            switch (peca.Genero)
            {
                case GeneroPeca.Tragedia:
                    totalPeca = 40000;
                    if (apresentacao.Publico > 30)
                        totalPeca += 1000 * (apresentacao.Publico - 30);
                    break;

                case GeneroPeca.Comedia:
                    totalPeca = 30000;
                    if (apresentacao.Publico > 20)
                        totalPeca += 10000 + 500 * (apresentacao.Publico - 20);
                    totalPeca += 300 * apresentacao.Publico;
                    break;

                default:
                    throw new Exception("Peça não catalogada");
            }

            creditorPorVolume += Math.Max(apresentacao.Publico - 30, 0);
            if (peca.Genero == GeneroPeca.Comedia)
                creditorPorVolume += Convert.ToInt16(Math.Floor(Convert.ToDecimal(apresentacao.Publico) / Convert.ToDecimal(5)));

            resultado += $"  {peca.Descricao}: ${totalPeca} ({apresentacao.Publico} assentos)\r\n";
            valorTotal += totalPeca;
        }

        resultado += $"Total devido é ${valorTotal}\r\n";
        resultado += $"Você recebeu {creditorPorVolume} créditos";

        return await Task.FromResult(resultado);
    }
}
