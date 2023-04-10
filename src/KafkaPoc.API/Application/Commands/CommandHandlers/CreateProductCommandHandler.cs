using KafkaPoc.API.Application.Events.Output;
using KafkaPoc.Domain.Models;
using MediatR;

namespace KafkaPoc.API.Commands.CommandHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IMediator _mediator;

        public CreateProductCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product { Name = request.Name, Price = request.Price };
            // Save product to database or in-memory storage
            await _mediator.Publish(new ProductCreatedEvent(product), cancellationToken);
            return product;
        }
    }
}
