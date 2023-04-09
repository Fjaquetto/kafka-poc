using KafkaPoc.API.Events;
using KafkaPoc.API.Services.DataContracts;
using KafkaPoc.Domain.Models;
using MediatR;
using Newtonsoft.Json;
using System.Threading;

namespace KafkaPoc.API.Config.Kafka
{
    public static class KafkaConsumerHostExtensions
    {
        public static async Task StartConsumer(IServiceProvider services, IHostEnvironment environment, string topic)
        {
            var consumerService = services.GetRequiredService<IKafkaConsumerService>();
            var mediator = services.GetRequiredService<IMediator>();
            var cancellationTokenSource = new CancellationTokenSource();

            await Task.Run(() =>
                consumerService.ConsumeAsync(topic, async message =>
                {
                    switch (topic)
                    {
                        case "product_created":
                            var product = JsonConvert.DeserializeObject<Product>(message);
                            await mediator.Publish(new ProductCreatedEvent(product));
                            break;
                        default:
                            throw new InvalidOperationException($"Unknown topic: {topic}");
                    }
                }, cancellationTokenSource.Token));
        }
    }
}
