namespace EcleticaBeerControl.Application.Members.Commands
{
    public record BaseBreweryCommand : BaseCommand
    {
        public required string BreweryId { get; set; }
    }
}
