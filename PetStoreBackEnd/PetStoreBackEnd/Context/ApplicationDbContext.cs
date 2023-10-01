using Microsoft.EntityFrameworkCore;
using PetStoreBackEnd.Entities;

namespace PetStoreBackEnd.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ProductEntity> Products { get; set; }
    }
}
