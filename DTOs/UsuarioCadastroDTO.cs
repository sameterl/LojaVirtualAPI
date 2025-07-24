using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace LojaVirtualAPI.DTOs
{
    public class UsuarioCadastroDTO
    {
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(6, ErrorMessage = "A senha deve conter no minímio 6 caracteres.")]
        public string Senha { get; set; } = string.Empty;

    }
    
}
