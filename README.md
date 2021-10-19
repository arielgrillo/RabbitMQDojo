# RabbitMQDojo
Dojo para pruebas pubvlisher & consumer en RabbitMQ. Antes que nada descargar la imagen docker.
> docker run -d --hostname my-rabbit --name ecomm-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management

## Documentación
https://www.rabbitmq.com/documentation.html

## Detalles de los puertos
Tal como indicamos en el comando de docker, mapeamos dos puertos.
- 15672 Para el admin, haciendo un http://localhost:15672 guest/guest
- 5672 Para el server, en dónde se conectaran los publishers y cosnumers

## Tipos de Exchanges
Fuente: https://lostechies.com/derekgreer/2012/03/28/rabbitmq-for-windows-exchange-types/
- Direct. Routes messages with a routing key equal to the routing key declared by the binding queue.
- Topic. Routes messages to queues whose routing key matches all, **or a portion** of a routing key.
- Headers. Routes messages based upon a matching of message headers to the expected headers specified by the binding queue.
- Fanout. Routes messages to all bound queues indiscriminately.  If a routing key is provided, it will simply be ignored.


