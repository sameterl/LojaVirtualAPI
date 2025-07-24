using LojaVirtualAPI.DTOs;
using LojaVirtualAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtualAPI.Controllers;

[ApiController]
[Route("usuarios")]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _service;
    private readonly AuthService _authService;

    public UsuarioController(UsuarioService service, AuthService authService)
    {
        _service = service;
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> CriarUsuario([FromBody] UsuarioCadastroDTO dto)
    {
        try
        {
            var usuario = await _service.CriarUsuarioAsync(dto);
            return CreatedAtAction(nameof(CriarUsuario), new { id = usuario.Id }, new { usuario.Id, usuario.Nome, usuario.Email });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        var token = await _authService.Autenticar(dto);
        if (token == null)
            return Unauthorized(new { message = "Email ou senha inválidos" });

        return Ok(new { token });
    }
}
