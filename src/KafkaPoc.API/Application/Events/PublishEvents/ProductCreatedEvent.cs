using KafkaPoc.Domain.Models;
using MediatR;

namespace KafkaPoc.API.Application.Events.Output
{
    public record ProductCreatedEvent(Product Product) : INotification;
}
