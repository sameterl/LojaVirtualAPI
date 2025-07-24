using LojaVirtualAPI.Data;
using LojaVirtualAPI.DTOs;
using LojaVirtualAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LojaVirtualAPI.Services;


public class UsuarioService
{
    private readonly AppDbContext _context;

    public UsuarioService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> EmailExiste(string email)
    {
        return await _context.Usuarios.AnyAsync(u => u.Email == email);
    }

    public string GerarHashSenha(string senha)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(senha);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);    
    }

    public async Task<Usuario> CriarUsuarioAsync(UsuarioCadastroDTO dto)
    {
        if (await EmailExiste(dto.Email))
            throw new Exception("Email já cadastrado");

        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            SenhaHash = GerarHashSenha(dto.Senha),
            CriadoEm = DateTime.Now
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return usuario;
    }
}
