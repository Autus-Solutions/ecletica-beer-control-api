namespace EcleticaBeerControl.Domain.Primitives
{
    public class BreweryUser
    {
        public Guid Id { get; set; }
        public Guid BreweryId { get; set; }
        public string BreweryName { get; set; } = string.Empty;
        public bool BreweryRegistred { get; set; }
        public bool Owner { get; set; }

        public Dictionary<string, object> ToUserMetadata()
        {
            return new Dictionary<string, object>
            {
                { "brewery_id", BreweryId.ToString() },
                { "brewery_name", BreweryName.ToString() },
                { "brewery_registred", BreweryRegistred.ToString() },
                { "owner", Owner.ToString() },
            };
        }

    }
}
