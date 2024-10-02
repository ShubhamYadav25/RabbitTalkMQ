using System.Text;
using System.Threading.Channels;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

// connection to the RabbitMQ server 
var factory = new ConnectionFactory { HostName = "localhost" };

var connection = factory.CreateConnection();

//
var channel = connection.CreateModel();

// consumer 
var consumer = new EventingBasicConsumer(channel);

Console.WriteLine("Consumer started...............................................\n");

consumer.Received += (m, arge) =>
{
    var receivedBody = arge.Body.ToArray();
    var receivedMsg = Encoding.UTF8.GetString(receivedBody);
    Console.WriteLine(receivedMsg);
};

channel.BasicConsume(queue: "ChatQueue", autoAck: true, consumer: consumer);

Console.ReadLine();
