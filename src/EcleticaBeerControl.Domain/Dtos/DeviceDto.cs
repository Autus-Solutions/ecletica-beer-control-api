using EcleticaBeerControl.Domain.Enums;

namespace EcleticaBeerControl.Domain.Dtos
{
    public record DeviceDto (Guid Id, string Identifier, Guid ClientId, string Name, string Description, DeviceStatus Status)
    {
    }
}
