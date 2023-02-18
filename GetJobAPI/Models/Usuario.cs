using System.ComponentModel.DataAnnotations;

namespace GetJobAPI.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Contraseña { get; set; }
        public string Email { get; set; }
        public string NumeroTelefono { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
