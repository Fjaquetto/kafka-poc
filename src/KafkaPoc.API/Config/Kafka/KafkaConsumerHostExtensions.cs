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
        public static async Task StartConsumer(IServiceProvider services, string topic)
        {
            var consumerService = services.GetRequiredService<IKafkaConsumerService>();
            var mediator = services.GetRequiredService<IMediator>();
            var cancellationTokenSource = new CancellationTokenSource();

            await Task.Run(() =>
                consumerService.ConsumeAsync(topic, async message =>
                {

                }, cancellationTokenSource.Token));
        }
    }
}
