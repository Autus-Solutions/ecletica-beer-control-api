using EcleticaBeerControl.Domain.Entities.Base;

namespace EcleticaBeerControl.Domain.Repositories
{
    public interface IBreweryEntityRepository<TBreweryEntity> where TBreweryEntity : ClientEntity
    {
        public Task Insert(TBreweryEntity entity, CancellationToken cancellationToken = default);
    }
}
