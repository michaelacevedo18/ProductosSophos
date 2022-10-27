using Microsoft.AspNetCore.Mvc.Rendering;

namespace Producto.Models.ViewModels
{
    public class SellProductVM
    {
        public SaleDetail Saledetail { get; set; }
        public List<SaleDetail> ListDetails { get; set; }        
        public IEnumerable<SelectListItem> ListProducts { get; set; }
    }
}
