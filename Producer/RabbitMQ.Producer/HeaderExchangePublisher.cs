﻿using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQ.Producer
{
    public static class HeaderExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            //Esto se debería llamar args o algo por el estilo ya que es la lista de argumentos que le pasaremos al exchange
            //Eliminar exchange y queue para que el cambio en los atributos tenga efecto
            var ttl = new Dictionary<string, object>
            {
                { "x-message-ttl",30000}//Time to live of message in miliseconds
            };

            channel.ExchangeDeclare("demo-header-exchange", type: ExchangeType.Headers, arguments:ttl);
            var count = 0;

            Console.WriteLine("Produced started");
            while (true)
            {
                var message = new { Name = "producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object> { { "account", "update" } };

                channel.BasicPublish("demo-header-exchange", string.Empty, properties, body);
                count++;
                Console.WriteLine($"Produced: {JsonConvert.SerializeObject(message)}");
                Thread.Sleep(1000);
            }
            
        }

    }
}
