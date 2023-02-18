using System.ComponentModel.DataAnnotations;

namespace GetJobAPI.Models
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Usuario { get; set; }

        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }

        public string Apellidos { get; set; }
        public string NumeroTelefono { get; set; }
    }
}
