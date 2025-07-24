namespace LojaVirtualAPI.Models;


public class Produto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public decimal Preco { get; set; }

    public string ImagemUrl { get; set; } = string.Empty;

    public int Estoque { get; set; }

    public bool Ativo { get; set; } = true; // para soft delete

    public DateTime CriadoEm { get; set; } = DateTime.Now;

}
