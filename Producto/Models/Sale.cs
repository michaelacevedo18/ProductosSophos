using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Producto.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
                
        public DateTime FechaOrden { get; set; }=DateTime.Now;

        public double TotalOrden { get; set; } = 0;

        public string EstadoOrden { get; set; }
    }
}
