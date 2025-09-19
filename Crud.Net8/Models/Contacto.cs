using System.ComponentModel.DataAnnotations;

namespace Crud.Net8.Models
{
    public class Contacto
    {
        [Key]//le decimos que es la llave y es auto incremental 
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")] //este campo es requerido 
        public String Nombre { get; set; }

        [Required(ErrorMessage = "El celular es obligatorio")] //este campo es requerido 
        public String Celular { get; set; }

        [Required(ErrorMessage = "El Email es obligatorio")] //este campo es requerido 
        public string Email { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
