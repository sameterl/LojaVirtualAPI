using LojaVirtualAPI.Data;
using LojaVirtualAPI.DTOs;
using LojaVirtualAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtualAPI.Services;

public class PedidoService
{
    private readonly AppDbContext _context;
    private readonly CarrinhoService _carrinhoService;

    public PedidoService(AppDbContext context, CarrinhoService carrinhoService)
    {
        _context = context;
        _carrinhoService = carrinhoService;
    }

    public async Task<Pedido?> CriarPedidoAsync(int usuarioId)
    {
        var carrinho = await _context.Carrinhos
            .Include(c => c.Itens)
            .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

        if (carrinho == null || carrinho.Itens.Count == 0)
            return null; 

        var pedido = new Pedido
        {
            UsuarioId = usuarioId,
            DataCriacao = DateTime.Now,
            Itens = new List<ItemPedido>()
        };

        decimal total = 0;

        foreach (var itemCarrinho in carrinho.Itens)
        {
            var itemPedido = new ItemPedido
            {
                ProdutoId = itemCarrinho.ProdutoId,
                Quantidade = itemCarrinho.Quantidade,
                PrecoUnitario = itemCarrinho.Produto.Preco
            };

            total += itemPedido.PrecoUnitario * itemPedido.Quantidade;
            pedido.Itens.Add(itemPedido);
        }

        pedido.Total = total;

        _context.Pedidos.Add(pedido);

        _context.ItensCarrinho.RemoveRange(carrinho.Itens);
        carrinho.Itens.Clear();

        await _context.SaveChangesAsync();

        return pedido;
    }

    public async Task<List<PedidoDTO>> ListarPedidosAsync(int usuarioId)
    {
        var pedidos = await _context.Pedidos
            .Where(p => p.UsuarioId == usuarioId)
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .OrderByDescending(p => p.DataCriacao)
            .ToListAsync();

        return pedidos.Select(p => new PedidoDTO
        {
            Id = p.Id,
            DataCriacao = p.DataCriacao,
            Total = p.Total,
            Itens = p.Itens.Select(i => new ItemPedidoDTO
            {
                ProdutoId = i.ProdutoId,
                NomeProduto = i.Produto.Nome,
                Quantidade = i.Quantidade,
                PrecoUnitario = i.PrecoUnitario
            }).ToList()
        }).ToList();
    }
    public async Task<PedidoDTO?> ObterPedidoPorIdAsync(int usuarioId, int pedidoId)
    {
        var pedido = await _context.Pedidos
            .Where(p => p.UsuarioId == usuarioId && p.Id == pedidoId)
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync();

        if (pedido == null) return null;

        return new PedidoDTO
        {
            Id = pedido.Id,
            DataCriacao = pedido.DataCriacao,
            Total = pedido.Total,
            Itens = pedido.Itens.Select(i => new ItemPedidoDTO
            {
                ProdutoId = i.ProdutoId,
                NomeProduto = i.Produto.Nome,
                Quantidade = i.Quantidade,
                PrecoUnitario = i.PrecoUnitario
            }).ToList()
        };
    }
}
