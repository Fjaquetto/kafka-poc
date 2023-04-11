using KafkaPoc.Console.Application.Events.MessageHandler;
using KafkaPoc.Console.Services.DataContracts;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaPoc.Console.Config.Kafka
{
    public static class KafkaConsumerHostExtensions
    {
        public static async Task StartConsumer(IServiceProvider services, string topic)
        {
            var consumerService = services.GetRequiredService<IKafkaConsumerService>();
            var messageHandlers = services.GetServices<IKafkaMessageHandler>();
            var handler = messageHandlers.FirstOrDefault(h => h.Topic == topic);
            var cancellationTokenSource = new CancellationTokenSource();

            if (handler == null)
            {
                return;
            }

            await Task.Run(() =>
                consumerService.ConsumeAsync(topic, handler.HandleMessageAsync, cancellationTokenSource.Token));
        }
    }
}
