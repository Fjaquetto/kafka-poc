using KafkaPoc.API.Application.Events.MessageHandler;
using KafkaPoc.API.Services.DataContracts;

namespace KafkaPoc.API.Config.Kafka
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
