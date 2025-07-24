using LojaVirtualAPI.DTOs;
using LojaVirtualAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LojaVirtualAPI.Controllers;

[ApiController]
[Route("pedidos")]
[Authorize]
public class PedidoController : ControllerBase
{
    private readonly PedidoService _pedidoService;

    public PedidoController(PedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    private int ObterUsuarioId()
    {
        var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
        return int.Parse(usuarioIdClaim!.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CriarPedido()
    {
        var usuarioId = ObterUsuarioId();

        var pedido = await _pedidoService.CriarPedidoAsync(usuarioId);

        if (pedido == null)
            return BadRequest(new { message = "Carrinho vazio, não é possível criar pedido." });

        return CreatedAtAction(nameof(ObterPedidoPorId), new { id = pedido.Id }, pedido);
    }

    [HttpGet]
    public async Task<IActionResult> ListarPedidos()
    {
        var usuarioId = ObterUsuarioId();
        var pedidos = await _pedidoService.ListarPedidosAsync(usuarioId);
        return Ok(pedidos);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPedidoPorId(int id)
    {
        var usuarioId = ObterUsuarioId();
        var pedido = await _pedidoService.ObterPedidoPorIdAsync(usuarioId, id);

        if (pedido == null)
            return NotFound();

        return Ok(pedido);
    }
}
