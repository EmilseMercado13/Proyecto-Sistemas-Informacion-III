using System.ComponentModel.DataAnnotations;

namespace ProyectoSistemasIII.Models
{
    public class Libro
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El autor es obligatorio.")]
        public string Autor { get; set; }

        [Required(ErrorMessage = "El ISBN es obligatorio.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "El año de publicación es obligatorio.")]
        [Range(1450, 2024, ErrorMessage = "El año de publicación debe ser entre 1450 y 2024.")]
        public int AnoPublicacion { get; set; }

        [Required(ErrorMessage = "El género es obligatorio.")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "El número de páginas es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de páginas debe ser mayor a 0.")]
        public int NumeroPaginas { get; set; }

        [Required(ErrorMessage = "El idioma es obligatorio.")]
        public string Idioma { get; set; }

        [Required(ErrorMessage = "El editorial es obligatorio.")]
        public string Editorial { get; set; }

        public bool Disponible { get; set; } = true;
    }
}
