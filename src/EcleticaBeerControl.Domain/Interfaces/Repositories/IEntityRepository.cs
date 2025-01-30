using EcleticaBeerControl.Domain.Entities.Base;

namespace EcleticaBeerControl.Domain.Interfaces.Repositories
{
    public interface IEntityRepository<TEntity> where TEntity : Entity
    {
        public Task Insert(TEntity entity, CancellationToken cancellationToken = default);
    }
}
