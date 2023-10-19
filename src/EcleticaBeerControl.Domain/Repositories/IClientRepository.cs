using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Domain.Entities.Base;

namespace EcleticaBeerControl.Domain.Repositories
{
    public interface IClientRepository<IClientEntity> where IClientEntity : ClientEntity
    {
        public Task Insert(IClientEntity entity, CancellationToken cancellationToken = default);
    }
}
