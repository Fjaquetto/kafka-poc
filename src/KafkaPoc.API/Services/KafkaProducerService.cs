using Confluent.Kafka;
using KafkaPoc.API.Config.Kafka;
using KafkaPoc.API.Services.DataContracts;
using Microsoft.Extensions.Configuration;

namespace KafkaPoc.API.Services
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaProducerService(IConfiguration configuration)
        {
            var kafkaConfig = configuration.GetSection("Kafka").Get<KafkaConfig>();

            var config = new ProducerConfig { BootstrapServers = kafkaConfig.BootstrapServers };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task ProduceAsync(string topic, string message)
        {
            await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
        }
    }
}
