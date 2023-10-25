namespace EcleticaBeerControl.Application.Members.Commands
{
    public record BaseBreweryCommand : BaseCommand
    {
        public Guid BreweryId { get; set; }
    }
}
