using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Consumer
{
    public static class TopicExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("demo-topic-exchange", ExchangeType.Topic);
            channel.QueueDeclare("demo-topic-queue",durable: true,exclusive: false,autoDelete: false,arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            channel.QueueBind("demo-topic-queue", "demo-topic-exchange", "account.*");
            channel.BasicQos(0, 10, false);
            //Eliminar exchange y queue para que el cambio en los atributos tenga efecto

            channel.BasicConsume("demo-topic-queue", true, consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();

        }
    }
}
