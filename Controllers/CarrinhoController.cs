using LojaVirtualAPI.DTOs;
using LojaVirtualAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LojaVirtualAPI.Controllers;

[ApiController]
[Route("carrinho")]
[Authorize]
public class CarrinhoController : ControllerBase
{
    private readonly CarrinhoService _service;

    public CarrinhoController(CarrinhoService service)
    {
        _service = service;
    }

    private int ObterUsuarioId()
    {
        var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
        return int.Parse(usuarioIdClaim!.Value);
    }

    [HttpGet]
    public async Task<IActionResult> ListarItens()
    {
        var usuarioId = ObterUsuarioId();
        var itens = await _service.ListarItensAsync(usuarioId);
        return Ok(itens);
    }

    [HttpPost("adicionar")]
    public async Task<IActionResult> AdicionarItem([FromBody] AdicionarItemDTO dto)
    {
        var usuarioId = ObterUsuarioId();
        await _service.AdicionarItemAsync(usuarioId, dto);
        return Ok(new { message = "Item adicionado ao carrinho" });
    }

    [HttpPost("atualizar")]
    public async Task<IActionResult> AtualizarQuantidade([FromBody] AtualizarItemDTO dto)
    {
        var usuarioId = ObterUsuarioId();
        var sucesso = await _service.AtualizarQuantidadeAsync(usuarioId, dto);
        if (!sucesso) return NotFound(new { message = "Item não encontrado no carrinho" });
        return Ok(new { message = "Quantidade atualizada" });
    }

    [HttpPost("remover")]
    public async Task<IActionResult> RemoverItem([FromBody] RemoverItemDTO dto)
    {
        var usuarioId = ObterUsuarioId();
        var sucesso = await _service.RemoverItemAsync(usuarioId, dto.ProdutoId);
        if (!sucesso) return NotFound(new { message = "Item não encontrado no carrinho" });
        return Ok(new { message = "Item removido do carrinho" });
    }

    [HttpDelete("limpar")]
    public async Task<IActionResult> LimparCarrinho()
    {
        var usuarioId = ObterUsuarioId();
        await _service.LimparCarrinhoAsync(usuarioId);
        return Ok(new { message = "Carrinho limpo" });
    }
}
