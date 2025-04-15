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
            
        
    }
}
