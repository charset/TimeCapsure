using System.Xml.Linq;

namespace TimeCapsure.Configuration {
    public sealed class Exchange {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public bool Durable { get; private set; }
        public bool AutoDelete { get; private set; }

        public static Exchange Parse(XElement element) {
            string name = element.Attribute("Name").Value;
            string type = element.Attribute("Type").Value;
            bool durable = bool.Parse(element.Attribute("Durable").Value);
            bool autoDelete = bool.Parse(element.Attribute("AutoDelete").Value);

            return new Exchange() {
                Name = name,
                Type = type,
                Durable = durable,
                AutoDelete = autoDelete
            };
        }
    }
}
