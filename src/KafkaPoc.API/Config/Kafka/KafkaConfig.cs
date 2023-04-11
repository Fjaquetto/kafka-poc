using KafkaPoc.API.Application.Events.MessageHandler;

namespace KafkaPoc.API.Config.Kafka
{
    public class KafkaConfig
    {
        public string BootstrapServers { get; set; }
        public string ConsumerGroupId { get; set; }
        public IList<IKafkaMessageHandler> MessageHandlers { get; set; } = new List<IKafkaMessageHandler>();
    }
}
