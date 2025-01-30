namespace EcleticaBeerControl.Domain.Interfaces.Services
{
    public interface IBreweryResolver
    {
        string? BreweryId { get; set; }
        public Task<bool> SetBrewery(string? userName);
    }
}
