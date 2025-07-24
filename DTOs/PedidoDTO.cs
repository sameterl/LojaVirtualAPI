namespace LojaVirtualAPI.DTOs;

public class PedidoDTO
{
    public int Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public decimal Total { get; set; }
    public List<ItemPedidoDTO> Itens { get; set; } = new List<ItemPedidoDTO>();
}
