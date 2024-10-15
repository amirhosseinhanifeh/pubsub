using MassTransit;
using Publisher;

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
            // ارسال پیام
            Console.WriteLine("Please Enter Your Message ...");
            await busControl.Publish(new NotificationMessage { Text = Console.ReadLine().ToString() });
            Console.WriteLine("Message Published");
            Console.ReadLine();

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
        });
    }
}
