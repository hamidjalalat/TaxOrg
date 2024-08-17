using Dtat.Ddd;
using MediatR;
using System.Collections.Generic;

namespace Domain.Rich.SeedWork
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        protected AggregateRoot() : base()
        {
            _domainEvents =
                new List<IDomainEvent>();
        }

        // **********
        [System.Text.Json.Serialization.JsonIgnore]
        private readonly List<IDomainEvent> _domainEvents;

        [System.Text.Json.Serialization.JsonIgnore]
        public IReadOnlyList<IDomainEvent> DomainEvents
        {
            get
            {
                return _domainEvents;
            }
        }
        // **********

        public void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            if (domainEvent is null)
            {
                return;
            }

            // **************************************************
            _domainEvents?.Add(domainEvent);
            // **************************************************

            // **************************************************
            //if (_domainEvents is not null)
            //{
            //	_domainEvents.Add(domainEvent);
            //}
            // **************************************************
        }

        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            if (domainEvent is null)
            {
                return;
            }

            // **************************************************
            _domainEvents?.Remove(domainEvent);
            // **************************************************

            // **************************************************
            //if (_domainEvents is not null)
            //{
            //	_domainEvents.Add(domainEvent);
            //}
            // **************************************************
        }

        public void ClearDomainEvents()
        {
            // **************************************************
            _domainEvents?.Clear();
            // **************************************************

            // **************************************************
            //if (_domainEvents is not null)
            //{
            //	_domainEvents.Clear();
            //}
            // **************************************************
        }
    }
}
