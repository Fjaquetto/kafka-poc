namespace KafkaPoc.API.Services.DataContracts
{
    public interface IKafkaProducerService
    {
        Task ProduceAsync(string topic, string message);
    }
}
