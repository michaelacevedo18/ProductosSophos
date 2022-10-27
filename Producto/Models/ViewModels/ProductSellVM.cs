using Microsoft.AspNetCore.Mvc.Rendering;

namespace Producto.Models.ViewModels
{
    public class ProductSellVM
    {        
        public Sale Sale { get; set; }
        public IEnumerable<SelectListItem> ClientesLista { get; set; }        
    }
}
