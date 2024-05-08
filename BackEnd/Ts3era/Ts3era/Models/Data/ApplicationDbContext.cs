using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ts3era.Dto;

namespace Ts3era.Models.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
             
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
             
        }
       



        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }   
        public DbSet<Product> Products { get; set; }   
        public DbSet<Governorates> Governorates { get; set;}
        public DbSet<Ports> Ports { get; set; }
        public DbSet<PortTypes> PortTypes { get; set; } 
        public DbSet<Complaints> Complaints { get; set; }
        public DbSet<FavoriteProductUser> FavoriteProducts { get; set;}
  
    }

}
