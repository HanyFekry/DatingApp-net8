using DatingApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApi.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<AppUser> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>()
                .HasData(new List<AppUser>
                {
                    new AppUser
                    {
                        Id=1,
                        Name="Hany"
                    },
                    new AppUser
                    {
                        Id=2,
                        Name="Basem"
                    },
                    new AppUser
                    {
                        Id=3,
                        Name="Mohamed"
                    }
                });
            base.OnModelCreating(builder);
        }
    }
}
