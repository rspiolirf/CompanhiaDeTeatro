using CompanhiaDeTeatro.Dominio.Enumeracoes;

namespace CompanhiaDeTeatro.Dominio.Entidades;

public class Peca
{
    public int CodigoPeca { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public GeneroPeca Genero { get; set; }
}