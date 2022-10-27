using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Producto.Data;
using Producto.Models;

namespace Producto.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Upsert(int? id)
        {

            Product product = new();

            if (id == null)
            {
                return View(product);
            }
            product = _context.Productos.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Upsert(Product product)//tambien se lo puedo pasar null
        {
            if (ModelState.IsValid)
            {
                if (product.Id == 0)
                {
                    _context.Productos.Add(product);
                }
                else
                {
                    _context.Productos.Update(product);
                }
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var productos = await _context.Productos.ToListAsync();

            try
            {
                return Json(new { data = productos });
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Exceptions" + e);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var objDb = await _context.Productos.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (objDb == null)
            {
                return Json(new { success = false, message = "Error al Borrar el producto" });
            }
            _context.Productos.Remove(objDb);
            _context.SaveChanges();
            return Json(new { success = true, message = "Producto eliminado con éxito" });
        }
        #endregion
    }
}
