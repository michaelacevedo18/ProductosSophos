using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Producto.Models
{
    public class SaleProduct
    {
        public SaleProduct()
        {
            Cantidad = 1;
        }
        [Key]
        public int MyProperty { get; set; }
        //
        [Required]
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        //
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        //
        [Required]
        [Range(1, 1000, ErrorMessage = "Ingresar un valor de 1 al 1000")]
        public int Cantidad { get; set; }

        [NotMapped]
        public double Precio { get; set; }
    }
}
