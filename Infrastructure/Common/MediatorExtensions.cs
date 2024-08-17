using System.Linq;

using Dtat.Ddd;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Common
{
    public static class MediatorExtensions
    {
        public static async Task DispatchDomainEvents(this IMediator mediator, DbContext context)
        {

            var aggregateRoots =
                    context.ChangeTracker.Entries()
                    .Where(current => current.Entity is IAggregateRoot)
                    .Select(current => current.Entity as IAggregateRoot)
                    .ToList()
                    ;

            foreach (var aggregateRoot in aggregateRoots)
            {
                // Dispatch Events!
                foreach (var domainEvent in aggregateRoot.DomainEvents)
                {
                    await mediator.Publish(domainEvent);
                }

                // Clear Events!
                aggregateRoot.ClearDomainEvents();
            }
        }
    }
}