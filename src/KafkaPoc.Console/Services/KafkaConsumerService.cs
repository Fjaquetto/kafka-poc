using Confluent.Kafka;
using KafkaPoc.Console.Config.Kafka;
using KafkaPoc.Console.Services.DataContracts;
using Microsoft.Extensions.Configuration;

namespace KafkaPoc.Console.Services
{
    public class KafkaConsumerService : IKafkaConsumerService
    {
        private readonly IConsumer<Ignore, string> _consumer;

        public KafkaConsumerService(IConfiguration configuration)
        {
            var kafkaConfig = configuration.GetSection("Kafka").Get<KafkaConfig>();

            var config = new ConsumerConfig
            {
                GroupId = kafkaConfig.ConsumerGroupId,
                BootstrapServers = kafkaConfig.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        }

        public async Task ConsumeAsync(string topic, Func<string, Task> messageHandler, CancellationToken cancellationToken)
        {
            _consumer.Subscribe(topic);

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = _consumer.Consume(cancellationToken);
                        await messageHandler(consumeResult.Message.Value);
                    }
                    catch (ConsumeException ex)
                    {
                        // Log the consume exception
                        //Console.WriteLine($"Error while consuming message: {ex.Error.Reason}");
                    }
                    catch (OperationCanceledException)
                    {
                        // The loop was canceled, break out of the loop
                        break;
                    }
                }
            }
            finally
            {
                _consumer.Close();
            }
        }
    }
}
