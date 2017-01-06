using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Reflection;

namespace OTInfo
{
    public class ServerInformation
    {
        private string host;
        private int port;
        private static int cache = 120;
        private string path;
        private Server server = new Server();

        public Server.ServerInfo serverinfo { get; set; }
        public Server.Owner owner { get; set; }
        public Server.Players players { get; set; }
        public Server.Monsters monsters { get; set; }
        public Server.Npcs npcs { get; set; }
        public Server.Rates rates { get; set; }
        public Server.Map map { get; set; }
        public string motd { get; set; }

        public ServerInformation(string host, int port = 7171)
        {
            this.host = host;
            this.port = port;

            path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + "cache";
        }

        public bool Execute()
        {
            string cache_uri = path + Path.DirectorySeparatorChar + host + ".json";
            if (cache > 0 && File.Exists(cache_uri) && File.GetLastWriteTime(cache_uri).AddSeconds(cache) >= DateTime.Now)
            {
                string json = File.ReadAllText(cache_uri);
                server = JsonConvert.DeserializeObject<Server>(json);
                setInfo(server);
                return true;
            }
            else
            {
                try
                {
                    TcpClient client = new TcpClient(host, port);
                    NetworkStream stream = client.GetStream();

                    stream.Write(new byte[] { 6, 0, 255, 255 }, 0, 4);
                    byte[] message = Encoding.UTF8.GetBytes("info");
                    stream.Write(message, 0, message.Length);

                    byte[] data = new byte[1024];
                    stream.Read(data, 0, data.Length);
                    string xml = Encoding.UTF8.GetString(data);
                    xml = xml.Replace("\n", "").Replace("\0", "");

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);

                    XmlNode node = doc.LastChild;

                    server = JsonConvert.DeserializeObject<Server>(JsonConvert.SerializeXmlNode(node));

                    var json = JsonConvert.SerializeObject(server);
                    File.WriteAllText(cache_uri, json);
                    setInfo(server);

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
   
        private void setInfo(Server server)
        {
            serverinfo = server.tsqp.serverinfo;
            owner = server.tsqp.owner;
            players = server.tsqp.players;
            monsters = server.tsqp.monsters;
            npcs = server.tsqp.npcs;
            rates = server.tsqp.rates;
            map = server.tsqp.map;
            motd = server.tsqp.motd;
        }

        public void setPath(string path)
        {
            this.path = path;
        }
    }
}
