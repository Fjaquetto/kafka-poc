namespace KafkaPoc.API.Config.Kafka
{
    public class KafkaConfig
    {
        public string BootstrapServers { get; set; }
        public string ConsumerGroupId { get; set; }
        public List<string> Topics { get; set; }
    }
}
