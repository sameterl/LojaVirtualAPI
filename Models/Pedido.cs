using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtualAPI.Models;

public class Pedido
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UsuarioId { get; set; }

    [ForeignKey("UsuarioId")]
    public Usuario Usuario { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.Now;

    public decimal Total { get; set; }

    public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
}
