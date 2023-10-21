namespace EcleticaBeerControl.Application.Members.Commands
{
    public record BaseCommand
    {
        public Guid CreateBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
