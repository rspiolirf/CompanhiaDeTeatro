namespace CompanhiaDeTeatro.Dominio.Entidades;

public class Fatura
{
    public int Codigo { get; set; }

    public string Cliente { get; set; } = string.Empty;

    public IEnumerable<Apresentacao> Apresentacoes { get; set; } = new List<Apresentacao>();
}
