namespace KafkaPoc.Console.Application.Events.MessageHandler
{
    public interface IKafkaMessageHandler
    {
        string Topic { get; }
        Task HandleMessageAsync(string message);
    }
}
