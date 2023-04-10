using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaPoc.Console.Services.DataContracts
{
    public interface IKafkaConsumerService
    {
        Task ConsumeAsync(string topic, Func<string, Task> messageHandler, CancellationToken cancellationToken);
    }
}
