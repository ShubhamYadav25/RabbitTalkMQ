using System.Text;
using System.Threading.Channels;
using RabbitMQ.Client;

// connection to the RabbitMQ server 
var factory = new ConnectionFactory { HostName = "localhost" };

var connection = factory.CreateConnection();

// creating channel within the established RabbitMQ connection
using (var channel = connection.CreateModel())
{
    // Producer start
    Console.WriteLine("Producer started...............................................\n");

    while (true)
    {
        Console.WriteLine("Write your message > ");
        string pubMsg =  Console.ReadLine();
        
        if(pubMsg == "exit")
        {
            break;
        }

        // convert it to byte code as RMQ communicate in byte
        var byteMsg = Encoding.UTF8.GetBytes(pubMsg);

        // now publish this msg 
        channel.BasicPublish(
            exchange: "ChatExchange",
            routingKey: "PubSubKey",
            basicProperties: null,
            body: byteMsg
        );

        Console.WriteLine("Msg published > "+ pubMsg);
    }


}
