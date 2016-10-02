using System;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace OTInfo
{
    public class ServerInformation
    {
        private string host;
        private int port;
        private static int cache = 120;
        private string path;

        public Server Info = new Server();

        public ServerInformation(string host, int port = 7171)
        {
            this.host = host;
            this.port = port;

            path = HttpRuntime.AppDomainAppPath + Path.DirectorySeparatorChar + "cache";
        }

        public bool Execute()
        {
            string cache_uri = path + Path.DirectorySeparatorChar + host + ".json";
            if (cache > 0 && File.Exists(cache_uri) && File.GetLastWriteTime(cache_uri).AddSeconds(cache) >= DateTime.Now)
            {
                string json = File.ReadAllText(cache_uri);
                Info = JsonConvert.DeserializeObject<Server>(json);
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

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);

                    XmlNode node = doc.LastChild;

                    Info = JsonConvert.DeserializeObject<Server>(JsonConvert.SerializeXmlNode(node));

                    var json = JsonConvert.SerializeObject(Info);
                    File.WriteAllText(cache_uri, json);

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public void setPath(string path)
        {
            this.path = path;
        }
    }
}
