namespace EcleticaBeerControl.Application.Members.Commands
{
    public record BaseCommand
    {
        public required string CreateBy { get; set; }
    }
}
