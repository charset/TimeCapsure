using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeCapsure {
    public class MessageSender {
        static MessageSender() {
            factory = new ConnectionFactory() {
                HostName = Config.Connection.HostName,
                UserName = Config.Connection.UserName,
                Password = Config.Connection.Password,
                VirtualHost = Config.Connection.VirtualHost
            };
        }

        private static QueueDeclareOk DeclareQueue(IModel model, string queueConfigName, Dictionary<string, object> arguments) {
            var queueConfig = Config.QueueDefinition.Queues[queueConfigName];

            return model.QueueDeclare(queue: queueConfig.Name,
                durable: queueConfig.Durable,
                exclusive: queueConfig.Exclusive,
                autoDelete: queueConfig.AutoDelete,
                arguments: arguments);
        }

        private readonly static ConnectionFactory factory;

        public static Guid Send(string queueName, string routingKey, object message) {
            using (var connection = factory.CreateConnection()) {
                using (var model = connection.CreateModel()) {
                    var exchange = Config.QueueDefinition.Queues[queueName].Exchange;
                    model.ExchangeDeclare(exchange: exchange.Name, type: exchange.Type, durable: exchange.Durable, autoDelete: exchange.AutoDelete, null);
                    var queue = DeclareQueue(model, queueName, null);
                    byte[] body = Encoding.UTF8.GetBytes(message.ToString());
                    var basicProperties = model.CreateBasicProperties();
                    Guid guid = Guid.NewGuid();
                    basicProperties.MessageId = guid.ToString();
                    basicProperties.DeliveryMode = 2;
                    basicProperties.Expiration = "60000";
                    model.BasicPublish(exchange: exchange.Name, routingKey: routingKey, basicProperties: basicProperties, body: body);
                    return guid;
                }
            }
        }
    }
}
