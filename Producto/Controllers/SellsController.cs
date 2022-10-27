using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Producto.Data;
using Producto.Models;
using Producto.Models.ViewModels;

namespace Producto.Controllers
{
    public class SellsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SellsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ProductSellVM productoVM = new()
            {
                Sale = new(),
                ClientesLista = _context.Clientes.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            return View(productoVM);
        }

        [HttpPost]
        public IActionResult CreateSale(Sale sale)
        {
            if (ModelState.IsValid)
            {
                _context.Sales.Add(sale);
                _context.SaveChanges();
                return RedirectToAction("InsertEditDetails", "Sells", new { id = sale.Id });                
            }
            return View();
        }

        [HttpGet]
        public IActionResult InsertEditDetails(int? id)
        {
            SellProductVM sellVM = new()
            {
                Saledetail = new() { 
                    
                },
                ListProducts = _context.Productos.ToList().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            if (id == null)  //Es nuevo Reg
            {
                return View(sellVM);
            }
            else
            {
                sellVM.ListDetails = _context.SaleDetails.Where(x => x.SaleId == id).ToList();
                ViewBag.TotalCompra = sellVM.ListDetails.Sum(x => x.PrecioPorDetalle);
                ViewBag.IdVenta = id;
                return View(sellVM);
            }
        }
      
        [HttpPost]
        public IActionResult AddDetails(SaleDetail saleDetail)//tambien se lo puedo pasar null
        {
            if (ModelState.IsValid)
            {
                if (saleDetail.Id == 0)
                {
                    var productExist = _context.SaleDetails.FirstOrDefault(x => x.ProductId == saleDetail.ProductId && x.SaleId == saleDetail.SaleId);
                    double precioProducto = (_context.Productos.FirstOrDefault(x => x.Id == saleDetail.ProductId)).Price;
                    if (productExist != null)
                    {
                        int nuevaCantidad = productExist.Cantidad + saleDetail.Cantidad;
                        productExist.Cantidad = nuevaCantidad;
                        double valor_antiguo = productExist.PrecioPorDetalle;
                        double valor_nuevo = saleDetail.Cantidad * precioProducto;
                        double valor_actualizar = valor_antiguo + valor_nuevo;
                        productExist.PrecioPorDetalle = valor_actualizar;
                        _context.SaleDetails.Update(productExist);
                        _context.SaveChanges();
                        //Actualizo precio de compra
                        var objVenta =_context.Sales.FirstOrDefault(x => x.Id == saleDetail.SaleId);
                        double total = (_context.SaleDetails.Where(x => x.SaleId == saleDetail.SaleId).ToList()).Sum(x => x.PrecioPorDetalle);
                        objVenta.TotalOrden= total;
                        _context.Sales.Update(objVenta);
                        _context.SaveChanges();
                        return RedirectToAction("InsertEditDetails", "Sells", new { id = saleDetail.SaleId });
                    }
                   
                    double valor_nuevo0 = saleDetail.Cantidad * precioProducto;
                    saleDetail.PrecioPorDetalle = valor_nuevo0;
                    _context.SaleDetails.Add(saleDetail);
                }
                else
                {
                    _context.SaleDetails.Update(saleDetail);
                }
                _context.SaveChanges();
                //Actualizo precio de compra
                var objVenta1 = _context.Sales.FirstOrDefault(x => x.Id == saleDetail.SaleId);
                double total1 = (_context.SaleDetails.Where(x => x.SaleId == saleDetail.SaleId).ToList()).Sum(x => x.PrecioPorDetalle);
                objVenta1.TotalOrden = total1;
                _context.Sales.Update(objVenta1);
                _context.SaveChanges();
                return RedirectToAction("InsertEditDetails", "Sells", new { id= saleDetail.SaleId});
            }
            return View(saleDetail);
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {   
            var ventas = await _context.Sales.Include(x=>x.Customer).ToListAsync();
            try
            {
                return Json(new { data = ventas });
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Exceptions" + e);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var objDb = await _context.Sales.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (objDb == null)
            {
                return Json(new { success = false, message = "Error al Borrar el producto" });
            }
            _context.Sales.Remove(objDb);
            _context.SaveChanges();
            return Json(new { success = true, message = "Venta eliminada con éxito" });
        }
        #endregion
    }
}
