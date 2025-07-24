using LojaVirtualAPI.Data;
using LojaVirtualAPI.DTOs;
using LojaVirtualAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtualAPI.Services
{
    public class ProdutoService
    {
        private readonly AppDbContext _context;

        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Produto> CriarProdutoAsync(ProdutoCreateDTO dto)
        {
            var produto = new Produto
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Preco = dto.Preco,
                ImagemUrl = dto.ImagemUrl,
                Estoque = dto.Estoque,
                Ativo = true,
                CriadoEm = DateTime.Now
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<List<Produto>> ListarProdutosAsync(string? nomeFiltro, int pagina = 1, int tamanhoPagina = 10)
        {
            var query = _context.Produtos.AsQueryable();

            query = query.Where(p => p.Ativo);

            if (!string.IsNullOrEmpty(nomeFiltro))
            {
                query = query.Where(p => p.Nome.Contains(nomeFiltro));
            }

            return await query
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();
        }

        public async Task<Produto?> BuscarPorIdAsync(int id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id && p.Ativo);
        }


        public async Task<bool> AtualizarProdutoAsync(int id, ProdutoUpdateDTO dto)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id && p.Ativo);
            if (produto == null) return false;

            produto.Nome = dto.Nome;
            produto.Descricao = dto.Descricao;
            produto.Preco = dto.Preco;
            produto.ImagemUrl = dto.ImagemUrl;
            produto.Estoque = dto.Estoque;
            produto.Ativo = dto.Ativo;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletarProdutoAsync(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id && p.Ativo);
            if (produto == null) return false;

            produto.Ativo = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
