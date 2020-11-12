﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQ_Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var queueName = "demo-queue";

            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://munja:munja@localhost:5672/")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume(queueName, true, consumer);
            Console.ReadLine();
        }
    }
}
