using System.Text;
using System.Threading.Channels;
using RabbitMQ.Client;

// connection to the RabbitMQ server 
var factory = new ConnectionFactory { HostName = "localhost" };

var connection = factory.CreateConnection();

// creating channel within the established RabbitMQ connection
using (var channel = connection.CreateModel())
{
    for (int i = 0; i < 10000; i++)
    {
        // msg
        string pubMsg = "I am shubham";

        // convert it to byte code as RMQ communicate in byte
        var byteMsg = Encoding.UTF8.GetBytes(pubMsg);

        // now publish this msg 
        channel.BasicPublish(
            exchange: "ChatExchange",
            routingKey: "PubSubKey",
            basicProperties: null,
            body: byteMsg
        );

        Console.WriteLine("Msg published > " + i + " " + pubMsg);
    }

}