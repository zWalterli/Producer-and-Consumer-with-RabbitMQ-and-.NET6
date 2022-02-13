using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer.Service
{
    public static class QueueConsumer
    {
        private static readonly string QUEUE_NAME = "email";
        public static void Consume()
        {
            var _factory = new ConnectionFactory
            {
                HostName = "localhost"
                // Uri = new Uri("amqp://guest:3guest@localhost:15672")
            };

            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: QUEUE_NAME,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, e) => {
                        var body = e.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(message);
                    };

                    channel.BasicConsume(QUEUE_NAME, true, consumer);
                    Console.ReadLine();
                }
            }
        }
    }
}