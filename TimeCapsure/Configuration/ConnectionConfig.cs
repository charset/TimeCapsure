using System.Xml.Linq;

namespace TimeCapsure.Configuration {
    public sealed class ConnectionConfig {
        public string HostName { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string VirtualHost { get; private set; }

        public static ConnectionConfig Parse(XElement element) {
            string hostname = element.Attribute("HostName").Value;
            string username = element.Attribute("UserName").Value;
            string password = element.Attribute("Password").Value;
            string virtualhost = element.Attribute("VirtualHost").Value;

            return new ConnectionConfig() {
                HostName = hostname,
                UserName = username,
                Password = password,
                VirtualHost = virtualhost
            };
        }
    }
}

