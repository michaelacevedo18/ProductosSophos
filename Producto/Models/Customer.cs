using System.ComponentModel.DataAnnotations;

namespace Producto.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es requerido")]
        [Display(Name = "Nombre del cliente:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El apellido del cliente es requerido")]
        [Display(Name = "Apellido del cliente:")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El número de documento es requerido")]
        [StringLength(10, ErrorMessage = "El documento no puede exeder los 10 caracteres. ")]
        [Display(Name = "No. Documento:")]
        public string DocumentNumber { get; set; }

    }
}
