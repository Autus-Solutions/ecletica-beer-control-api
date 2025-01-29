namespace EcleticaBeerControl.Domain.Interfaces
{
    public interface IUser
    {
        string Id { get; }
        string BreweryId { get; set; }
        string BreweryName { get; set; }
        bool BreweryRegistred { get; set; }
        bool Owner { get; set; }
    }
}
