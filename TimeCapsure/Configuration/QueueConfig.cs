using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace TimeCapsure.Configuration {
    public sealed class QueueConfig {
        public string Name { get; private set; }
        public bool Durable { get; private set; }
        public bool Exclusive { get; private set; }
        public bool AutoDelete { get; private set; }

        //public Dictionary<string, Exchange> Exchanges { get; private set; }
        public Exchange Exchange { get; private set; }

        public static QueueConfig Parse(XElement element) {
            string queueName = element.Attribute("Name").Value;
            bool durable = bool.Parse(element.Attribute("Durable").Value);
            bool exclusive = bool.Parse(element.Attribute("Exclusive").Value);
            bool autoDelete = bool.Parse(element.Attribute("AutoDelete").Value);
            //var exchanges = element.Elements("ExchangeDefinition").Select(q => Exchange.Parse(q)).ToDictionary(key => key.Name, value => value);
            Exchange exchange = Exchange.Parse(element.Element("Exchange"));

            return new QueueConfig() {
                Name = queueName,
                Durable = durable,
                Exclusive = exclusive,
                AutoDelete = autoDelete,
                Exchange = exchange
            };
        }
    }
}
