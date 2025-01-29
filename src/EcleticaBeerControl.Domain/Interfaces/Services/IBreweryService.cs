namespace EcleticaBeerControl.Domain.Interfaces.Services
{
    public interface IBreweryService
    {
        string? BreweryId { get; set; }
        public Task<bool> SetBrewery(string? breweryId);
    }
}
