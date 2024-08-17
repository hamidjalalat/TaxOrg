
using Domain.Anemic.Common;
using Dtat.Ddd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
   
    public class EntityCreatedEvent<T> : BaseEvent
    {
        public EntityCreatedEvent(T item)
        {
            Item = item;
        }

        public T Item { get; }
    }
}
