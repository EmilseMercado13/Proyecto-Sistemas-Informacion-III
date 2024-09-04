using System.ComponentModel.DataAnnotations;

namespace ProyectoSistemasIII.DTOs
{
    public class PrestamoDto
    {
        [Required]
        public int LibroId { get; set; } // Asegúrate de que coincida con el nombre del campo en el modelo

        [Required]
        public int UsuarioId { get; set; } // Asegúrate de que coincida con el nombre del campo en el modelo

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }
    }
}
