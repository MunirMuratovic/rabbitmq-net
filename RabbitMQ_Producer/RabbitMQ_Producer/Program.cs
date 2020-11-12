using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace RabbitMQ_Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var queueName = "demo-queue";

            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var message = new { Name = "Product", Message = "Hello!" };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish("", queueName, null, body);
        }
    }
}
