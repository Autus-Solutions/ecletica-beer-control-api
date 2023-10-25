using EcleticaBeerControl.Domain.Enums;

namespace EcleticaBeerControl.Domain.Dtos
{
    public record DeviceDto (Guid Id, Guid BreweryId, string Identifier, string Name, string Description, DeviceStatus Status)
    {
    }
}
