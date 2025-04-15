using Microsoft.EntityFrameworkCore;
using MVC_AUTOSALONI.Models.Entities;

namespace MVC_AUTOSALONI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)  
        {

        }
        public DbSet <Marca> Marca { get; set; }

        public DbSet <Modello> Modello { get; set; }
        public DbSet <Auto> Auto { get; set; }
            
    }
}
