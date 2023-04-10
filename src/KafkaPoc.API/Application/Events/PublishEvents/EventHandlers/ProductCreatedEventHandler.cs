using KafkaPoc.API.Application.Events.Output;
using KafkaPoc.API.Services.DataContracts;
using MediatR;
using Newtonsoft.Json;

namespace KafkaPoc.API.Events.EventHandlers
{
    public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
    {
        private readonly IKafkaProducerService _producerService;

        public ProductCreatedEventHandler(IKafkaProducerService producerService)
        {
            _producerService = producerService;
        }

        public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _producerService.ProduceAsync(nameof(ProductCreatedEvent), JsonConvert.SerializeObject(notification.Product));
        }
    }
}
