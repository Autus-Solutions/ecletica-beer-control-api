using EcleticaBeerControl.Domain.Interfaces.Services;
using EcleticaBeerControl.Persistence.EF.Database;
using Microsoft.EntityFrameworkCore;

namespace EcleticaBeerControl.Application.Members.Services
{
    public class BreweryResolver(IdentityDbContext identityDbContext) : IBreweryResolver
    {
        private readonly IdentityDbContext _identityDbContext = identityDbContext;

        public string? BreweryId { get; set; }

        public async Task<bool> SetBrewery(string? userName)
        {
            var user = await _identityDbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user != null)
            {
                BreweryId = user.BreweryId;
                return true;
            }

            throw new Exception($"User '{userName ?? "EMPTY_USERNAME"}' not found!");
        }
    }
}
