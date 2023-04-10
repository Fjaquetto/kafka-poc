using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaPoc.Console.Config.Kafka
{
    public class KafkaConfig
    {
        public string BootstrapServers { get; set; }
        public string ConsumerGroupId { get; set; }
        public List<string> Topics { get; set; } = new List<string>();

        public KafkaConfig AddTopic(string topic)
        {
            Topics.Add(topic);
            return this;
        }
    }
}
