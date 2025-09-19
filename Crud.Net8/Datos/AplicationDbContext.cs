using Crud.Net8.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Crud.Net8.Datos
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) 
        {
            
        }

        //modelos - cada modelo corresponde a una tabla en una base de datos 
        public DbSet<Contacto> Contacto { get; set; }
    }
}
