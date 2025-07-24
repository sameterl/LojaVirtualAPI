namespace LojaVirtualAPI.DTOs;

public class ItemPedidoDTO
{
    public int ProdutoId { get; set; }
    public string NomeProduto { get; set; } = null!;
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
}
