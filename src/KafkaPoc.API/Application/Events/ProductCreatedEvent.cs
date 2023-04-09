using KafkaPoc.Domain.Models;
using MediatR;

namespace KafkaPoc.API.Events
{
    public record ProductCreatedEvent(Product Product) : INotification;
}
