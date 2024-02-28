namespace EcleticaBeerControl.Domain.Interfaces
{
    public interface IUser
    {
        Guid Id { get; set; }
        Guid BreweryId { get; set; }
        string BreweryName { get; set; }
        bool BreweryRegistred { get; set; }
        bool Owner { get; set; }
        Dictionary<string, object> ToUserMetadata();
    }
}
