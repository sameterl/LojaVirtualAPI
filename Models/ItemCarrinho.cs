using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtualAPI.Models;

public class ItemCarrinho
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int CarrinhoId { get; set; }

    [ForeignKey("CarrinhoId")]
    public Carrinho Carrinho { get; set; }

    [Required]
    public int ProdutoId { get; set; }

    [ForeignKey("ProdutoId")]
    public Produto Produto { get; set; }

    [Required]
    public int Quantidade { get; set; }
}
