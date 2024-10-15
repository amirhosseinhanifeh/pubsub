using MassTransit;
using Publisher;

public class NotificationConsumer : IConsumer<NotificationMessage>
{
    public Task Consume(ConsumeContext<NotificationMessage> context)
    {
        Console.WriteLine($"Received: {context.Message.Text}");
        return Task.CompletedTask;
    }
}
