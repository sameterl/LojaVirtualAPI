using System.ComponentModel.DataAnnotations;

namespace LojaVirtualAPI.DTOs;

public class ProdutoUpdateDTO
{
    [Required]
    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser positivo")]
    public decimal Preco { get; set; }

    public string ImagemUrl { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "Estoque não pode ser negativo")]
    public int Estoque { get; set; }

    public bool Ativo { get; set; }
}
