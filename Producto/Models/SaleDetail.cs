using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Producto.Models
{
    public class SaleDetail
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public int SaleId { get; set; }

        [ForeignKey("SaleId")]
        public Sale Sale { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        public int Cantidad { get; set; }

        public double PrecioPorDetalle { get; set; } = 0;
    }
}
