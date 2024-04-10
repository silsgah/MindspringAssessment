using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure
{
    public class BackContext : IdentityDbContext<User>
    {
        public BackContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TblResponse>  TblResponses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder
                .ApplyConfigurationsFromAssembly(Assembly
                    .GetExecutingAssembly());
            builder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "Manager", NormalizedName = "MANAGER" });
        }
    }
}
