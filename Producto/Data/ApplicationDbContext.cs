using Microsoft.EntityFrameworkCore;
using Producto.Models;
using System.Collections.Generic;

namespace Producto.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Clientes { get; set; }
        public DbSet<Product> Productos { get; set; }        
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
    }
}
