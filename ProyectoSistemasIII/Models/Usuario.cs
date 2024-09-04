using System.ComponentModel.DataAnnotations;

namespace ProyectoSistemasIII.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress]
        public string CorreoElectronico { get; set; }

        public bool EsAdmin { get; set; } = false;
    }
}
