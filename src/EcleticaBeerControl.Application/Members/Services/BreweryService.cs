using EcleticaBeerControl.Domain.Interfaces.Services;

namespace EcleticaBeerControl.Application.Members.Services
{
    public class BreweryService() : IBreweryService
    {
        public string? BreweryId { get; set; }

        public Task<bool> SetBrewery(string? breweryId)
        {
            throw new NotImplementedException();
        }
    }
}
