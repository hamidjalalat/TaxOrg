

using Domain.Anemic.Entities;
using Dtat.Ddd;

namespace Domain.Events
{
    public class MenuCompletedEvent : IDomainEvent
    {
        public MenuCompletedEvent(Menu item)
        {
            Item = item;
        }

        public Menu Item { get; }
    }
}