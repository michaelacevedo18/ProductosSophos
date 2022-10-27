using System.ComponentModel.DataAnnotations;

namespace Producto.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es requerido")]
        [Display(Name = "Nombre del producto:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El costo del producto es requerido")]
        [Display(Name = "Valor Unitario:")]
        public double Price { get; set; }
    }
}
