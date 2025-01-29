using EcleticaBeerControl.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EcleticaBeerControl.Domain.Models
{
    public class User : IdentityUser, IUser
    {
        public string BreweryId { get; set; } = string.Empty;
        public string BreweryName { get; set; } = string.Empty;
        public bool BreweryRegistred { get; set; }
        public bool Owner { get; set; }
    }
}
