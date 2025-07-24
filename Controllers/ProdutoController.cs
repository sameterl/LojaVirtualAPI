using LojaVirtualAPI.DTOs;
using LojaVirtualAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtualAPI.Controllers;

[ApiController] 
[Route("produtos")] 
public class ProdutoController : ControllerBase
{
    private readonly ProdutoService _service;

    public ProdutoController(ProdutoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] string? nome, [FromQuery] int pagina = 1, [FromQuery] int tamanho = 10)
    {
        var produtos = await _service.ListarProdutosAsync(nome, pagina, tamanho);
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var produto = await _service.BuscarPorIdAsync(id);
        if (produto == null) return NotFound();
        return Ok(produto);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Criar([FromBody] ProdutoCreateDTO dto)
    {
        var novo = await _service.CriarProdutoAsync(dto);
        return CreatedAtAction(nameof(ObterPorId), new { id = novo.Id }, novo);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Atualizar(int id, [FromBody] ProdutoUpdateDTO dto)
    {
        var sucesso = await _service.AtualizarProdutoAsync(id, dto);
        if (!sucesso) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Deletar(int id)
    {
        var sucesso = await _service.DeletarProdutoAsync(id);
        if (!sucesso) return NotFound();
        return NoContent();
    }
}
