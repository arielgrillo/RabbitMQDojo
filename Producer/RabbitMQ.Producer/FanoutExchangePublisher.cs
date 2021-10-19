using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQ.Producer
{
    public static class FanoutExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            //Esto se debería llamar args o algo por el estilo ya que es la lista de argumentos que le pasaremos al exchange
            //Eliminar exchange y queue para que el cambio en los atributos tenga efecto
            var ttl = new Dictionary<string, object>
            {
                { "x-message-ttl",30000}//Time to live of message in miliseconds
            };

            channel.ExchangeDeclare("demo-fanout-exchange", type: ExchangeType.Fanout, arguments:ttl);
            var count = 0;

            Console.WriteLine("Produced started");
            while (true)
            {
                var message = new { Name = "producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object> { { "account", "new" } };

                //Las properties las dejo para ver que no importa lo que le pases allí igual funciona.
                //Básicamente cualquier cosa que esté bindeada a esta cola funcionará.
                channel.BasicPublish("demo-fanout-exchange", "xzopenco", properties, body);
                count++;
                Console.WriteLine($"Produced: {JsonConvert.SerializeObject(message)}");
                Thread.Sleep(1000);
            }
            
        }

    }
}
