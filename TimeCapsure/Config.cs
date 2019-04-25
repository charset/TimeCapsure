using System.Xml.Linq;
using TimeCapsure.Configuration;

namespace TimeCapsure {
    public class Config {
            static Config() {
                string config = "timecapsure.xml";
                XDocument xDoc = XDocument.Load(config);
                Connection = ConnectionConfig.Parse(xDoc.Root.Element("Connection"));
                QueueDefinition = QueueDefinition.Parse(xDoc.Root.Element("QueueDefinition"));
            }

            public static ConnectionConfig Connection { get; private set; }
            public static QueueDefinition QueueDefinition { get; private set; }
        }
}
