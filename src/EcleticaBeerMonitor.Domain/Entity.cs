using System.ComponentModel;

namespace EcleticaBeerMonitor.Domain
{
    public class Entity
    {
        public Guid Id { get; protected set; }
        public Guid ClientId { get; protected set; }
    }
}