using LojaVirtualAPI.Data;
using LojaVirtualAPI.DTOs;
using LojaVirtualAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtualAPI.Services;

public class CarrinhoService
{
    private readonly AppDbContext _context;

    public CarrinhoService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Carrinho> ObterOuCriarCarrinhoAsync(int usuarioId)
    {
        var carrinho = await _context.Carrinhos
            .Include(c => c.Itens)
            .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

        if (carrinho == null)
        {
            carrinho = new Carrinho { UsuarioId = usuarioId };
            _context.Carrinhos.Add(carrinho);
            await _context.SaveChangesAsync();
        }

        return carrinho;
    }

    public async Task<List<ItemCarrinho>> ListarItensAsync(int usuarioId)
    {
        var carrinho = await ObterOuCriarCarrinhoAsync(usuarioId);
        return carrinho.Itens;
    }

    public async Task AdicionarItemAsync(int usuarioId, AdicionarItemDTO dto)
    {
        var carrinho = await ObterOuCriarCarrinhoAsync(usuarioId);

        var item = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == dto.ProdutoId);
        if (item != null)
        {
            item.Quantidade += dto.Quantidade;
        }
        else
        {
            item = new ItemCarrinho
            {
                CarrinhoId = carrinho.Id,
                ProdutoId = dto.ProdutoId,
                Quantidade = dto.Quantidade
            };
            carrinho.Itens.Add(item);
            _context.ItensCarrinho.Add(item);
        }

        await _context.SaveChangesAsync();
    }
    public async Task<bool> AtualizarQuantidadeAsync(int usuarioId, AtualizarItemDTO dto)
    {
        var carrinho = await ObterOuCriarCarrinhoAsync(usuarioId);

        var item = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == dto.ProdutoId);
        if (item == null) return false;

        item.Quantidade = dto.Quantidade;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoverItemAsync(int usuarioId, int produtoId)
    {
        var carrinho = await ObterOuCriarCarrinhoAsync(usuarioId);

        var item = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == produtoId);
        if (item == null) return false;

        carrinho.Itens.Remove(item);
        _context.ItensCarrinho.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task LimparCarrinhoAsync(int usuarioId)
    {
        var carrinho = await ObterOuCriarCarrinhoAsync(usuarioId);

        _context.ItensCarrinho.RemoveRange(carrinho.Itens);
        carrinho.Itens.Clear();

        await _context.SaveChangesAsync();
    }
}
