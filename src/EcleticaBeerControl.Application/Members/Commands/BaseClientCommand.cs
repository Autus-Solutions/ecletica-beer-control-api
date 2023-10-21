namespace EcleticaBeerControl.Application.Members.Commands
{
    public record BaseClientCommand : BaseCommand
    {
        public Guid ClientId { get; set; }
    }
}
