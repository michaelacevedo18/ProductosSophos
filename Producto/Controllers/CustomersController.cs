using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Producto.Data;
using Producto.Models;

namespace Producto.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomersController(ApplicationDbContext context)
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

            Customer cliente = new();
            
            if (id == null)
            {
                return View(cliente);
            }            
            cliente = _context.Clientes.FirstOrDefault(x=>x.Id==id);
            if (cliente == null) 
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]        
        public IActionResult Upsert(Customer cliente)//tambien se lo puedo pasar null
        {
            if (ModelState.IsValid)
            {
                if (cliente.Id == 0)
                {
                    _context.Clientes.Add(cliente);
                }
                else
                {
                    _context.Clientes.Update(cliente);                    
                }
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var clientes =await _context.Clientes.ToListAsync();

            try
            {
                return Json(new { data = clientes });
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Exceptions" + e);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {            
            var objDb = await _context.Clientes.Where(x=>x.Id== id).FirstOrDefaultAsync();
            if (objDb == null)
            {
                return Json(new { success = false, message = "Error al Borrar el cliente" });
            }
            _context.Clientes.Remove(objDb);
            _context.SaveChanges();
            return Json(new { success = true, message = "Cliente eliminado con éxito" });
        }
        #endregion
    }

}
