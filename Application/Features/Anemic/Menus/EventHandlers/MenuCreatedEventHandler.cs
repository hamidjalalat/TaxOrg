using Application.Common.Models;
using Domain.Anemic.Entities;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Features.Anemic.Menus.EventHandlers
{
    public class MenuCreatedEventHandler :  BaseNotificationHandler<EntityCreatedEvent<List<SendMessage>>>
    {

        protected override Task HandleNotificationAsync(EntityCreatedEvent<List<SendMessage>> input, CancellationToken cancellationToken)
        {

            throw new NotImplementedException();
        }
    }
}