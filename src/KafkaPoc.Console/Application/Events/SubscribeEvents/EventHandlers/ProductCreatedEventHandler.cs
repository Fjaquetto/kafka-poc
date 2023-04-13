using Kafka.Bus.Handlers;
using KafkaPoc.Console.Model;
using MediatR;
using Newtonsoft.Json;
using System.Threading;

namespace KafkaPoc.Console.Application.Events.EventHandlers
{
    public class ProductCreatedEventHandler : IKafkaMessageHandler
    {
        private readonly IMediator _mediator;

        public ProductCreatedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public string Topic => nameof(ProductCreatedEvent);

        public async Task HandleMessageAsync(string message, CancellationToken cancellationToken)
        {
            var product = JsonConvert.DeserializeObject<Product>(message);
            System.Console.WriteLine(JsonConvert.SerializeObject(message));
        }
    }
}
