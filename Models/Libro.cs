using System.ComponentModel.DataAnnotations;

namespace ApiNET.Models
{
    public class Libro
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(40, ErrorMessage = "El título no puede superar los 40 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El autor es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El autor solo puede contener letras y espacios.")]
        public string Autor { get; set; }
    }
}
