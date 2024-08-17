
using Dtat.Ddd;

namespace Domain.Events
{
    public class EntityCompletedEvent<T> : IDomainEvent
    {
        public EntityCompletedEvent(T item)
        {
            Item = item;
        }

        public T Item { get; }
    }
}