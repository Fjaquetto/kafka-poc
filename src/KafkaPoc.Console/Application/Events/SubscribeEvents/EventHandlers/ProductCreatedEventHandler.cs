using KafkaPoc.Console.Services.DataContracts;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaPoc.Console.Application.Events.EventHandlers
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
            System.Console.WriteLine(JsonConvert.SerializeObject(notification));
        }
    }
}
