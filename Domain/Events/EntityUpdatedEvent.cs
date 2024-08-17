
using Dtat.Ddd;

namespace Domain.Events
{
    public class EntityUpdatedEvent<T> : IDomainEvent
    {
        public EntityUpdatedEvent(T item)
        {
            Item = item;
        }

        public T Item { get; }
    }
}