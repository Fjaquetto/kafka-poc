using KafkaPoc.Domain.Models;
using MediatR;

namespace KafkaPoc.API.Commands
{
    public record CreateProductCommand(string Name, decimal Price) : IRequest<Product>;
}
