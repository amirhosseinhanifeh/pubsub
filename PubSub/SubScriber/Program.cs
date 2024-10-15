using MassTransit;

class Program
{
    private const string BASE_URL = "rabbitmq://localhost";
    private const string USERNAME = "guest";
    private const string PASSWORD = "guest";
    static async Task Main(string[] args)
    {
        var busControl = ConfigureBus();

        await busControl.StartAsync();
        try
        {
            Console.WriteLine("Waiting for messages...");
            await Task.Run(() => Console.ReadLine());
        }
        finally
        {
            await busControl.StopAsync();
        }
    }

    static IBusControl ConfigureBus()
    {
        return Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host(new Uri(BASE_URL), h =>
            {
                h.Username(USERNAME);
                h.Password(PASSWORD);
            });

            cfg.ReceiveEndpoint("notification_queue", e =>
            {
                e.Consumer<NotificationConsumer>();
            });
        });
    }
}
