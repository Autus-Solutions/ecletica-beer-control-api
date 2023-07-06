
using EcleticaBeerControl.Application.Domain.Entities;

namespace EcleticaBeerControl.Application.Repositories
{
    public interface IClientRepository
    {
        public Task<IList<Client>> GetAll();
    }
}
