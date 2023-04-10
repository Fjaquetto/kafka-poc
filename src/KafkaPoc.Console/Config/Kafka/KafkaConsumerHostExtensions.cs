using KafkaPoc.Console.Application.Events;
using KafkaPoc.Console.Model;
using KafkaPoc.Console.Services.DataContracts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace KafkaPoc.Console.Config.Kafka
{
    public static class KafkaConsumerHostExtensions
    {
        public static async Task StartConsumer(IServiceProvider services, string topic)
        {
            var consumerService = services.GetRequiredService<IKafkaConsumerService>();
            var mediator = services.GetRequiredService<IMediator>();
            var cancellationTokenSource = new CancellationTokenSource();

            await Task.Run(() =>
                consumerService.ConsumeAsync(topic, async message =>
                {
                    switch (topic)
                    {
                        case nameof(ProductCreatedEvent):
                            var product = JsonConvert.DeserializeObject<Product>(message);
                            await mediator.Publish(new ProductCreatedEvent(product));
                            break;
                    }
                }, cancellationTokenSource.Token));
        }
    }
}
