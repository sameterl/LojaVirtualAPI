using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LojaVirtualAPI.Models;

public class ItemPedido
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int PedidoId { get; set; }

    [JsonIgnore]
    [ForeignKey("PedidoId")]
    public Pedido Pedido { get; set; }

    [Required]
    public int ProdutoId { get; set; }

    [ForeignKey("ProdutoId")]
    public Produto Produto { get; set; }

    [Required]
    public int Quantidade { get; set; }

    [Required]
    public decimal PrecoUnitario { get; set; }
}
