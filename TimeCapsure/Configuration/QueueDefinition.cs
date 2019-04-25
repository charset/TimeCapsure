using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TimeCapsure.Configuration {
    public sealed class QueueDefinition {
        public Dictionary<string, QueueConfig> Queues { get; private set; }

        public static QueueDefinition Parse(XElement element) {
            return new QueueDefinition() {
                Queues = element.Elements("Queue").Select(q => QueueConfig.Parse(q)).ToDictionary(key => key.Name, value => value)
            };
        }
    }
}