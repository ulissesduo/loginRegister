using baltaIOCrud.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace baltaIOCrud.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base (options)
        {
            
        }

        public DbSet<SalesLeadEntity> SalesLead { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Category> Category { get; set; }

    }
}
