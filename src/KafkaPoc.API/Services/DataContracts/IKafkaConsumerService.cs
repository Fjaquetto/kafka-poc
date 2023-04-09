namespace KafkaPoc.API.Services.DataContracts
{
    public interface IKafkaConsumerService
    {
        Task ConsumeAsync(string topic, Func<string, Task> messageHandler, CancellationToken cancellationToken);
    }
}
