using EcleticaBeerControl.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcleticaBeerControl.Persistence.EF.Database
{
    public sealed class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : IdentityDbContext<User>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("identity");
        }
    }
}
