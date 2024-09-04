using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoSistemasIII.Models
{
    public class Prestamo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }

        public int LibroId { get; set; }
        [ForeignKey(nameof(LibroId))]
        public Libro Libro { get; set; }

        public int UsuarioId { get; set; }
        [ForeignKey(nameof(UsuarioId))]
        public Usuario Usuario { get; set; }
    }
}
