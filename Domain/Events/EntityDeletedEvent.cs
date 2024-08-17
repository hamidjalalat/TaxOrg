

using Dtat.Ddd;

namespace Domain.Events
{
    public class EntityDeletedEvent<T> : IDomainEvent
    {
        public EntityDeletedEvent(T item)
        {
            Item = item;
        }

        public T Item { get; }
    }
}