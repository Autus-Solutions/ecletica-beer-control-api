using EcleticaBeerControl.Domain.Entities;
using EcleticaBeerControl.Domain.Entities.Base;

namespace EcleticaBeerControl.Domain.Repositories
{
    public interface IEntityRepository<TEntity> where TEntity : Entity
    {
        public Task Insert(TEntity entity, CancellationToken cancellationToken = default);
    }
}
