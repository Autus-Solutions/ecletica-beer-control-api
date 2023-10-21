using EcleticaBeerControl.Domain.Enums;

namespace EcleticaBeerControl.Domain.Dtos
{
    public record DeviceDto (Guid Id, string Name, string Description, DeviceStatus Status)
    {
    }
}
