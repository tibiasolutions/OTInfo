using Newtonsoft.Json;

namespace OTInfo
{
    public class Server
    {
        public TSQP tsqp;

        public class TSQP
        {
            public ServerInfo serverinfo { get; set; }
            public Owner owner { get; set; }
            public Players players { get; set; }
            public Monsters monsters { get; set; }
            public Npcs npcs { get; set; }
            public Rates rates { get; set; }
            public Map map { get; set; }
            public string motd { get; set; }
        }

        public class ServerInfo
        {
            [JsonProperty("@uptime")]
            public int uptime { get; set; }

            [JsonProperty("@ip")]
            public string ip { get; set; }

            [JsonProperty("@servername")]
            public string servername { get; set; }

            [JsonProperty("@port")]
            public int port { get; set; }

            [JsonProperty("@location")]
            public string location { get; set; }

            [JsonProperty("@url")]
            public string url { get; set; }

            [JsonProperty("@server")]
            public string server { get; set; }

            [JsonProperty("@version")]
            public string version { get; set; }

            [JsonProperty("@client")]
            public string client { get; set; }
        }

        public class Owner
        {
            [JsonProperty("@name")]
            public string name { get; set; }

            [JsonProperty("@email")]
            public string email { get; set; }
        }

        public class Players
        {
            [JsonProperty("@online")]
            public int online { get; set; }

            [JsonProperty("@max")]
            public int max { get; set; }

            [JsonProperty("@peak")]
            public int peak { get; set; }
        }

        public class Monsters
        {
            [JsonProperty("@total")]
            public int total { get; set; }
        }

        public class Npcs
        {
            [JsonProperty("@total")]
            public int total { get; set; }
        }

        public class Rates
        {
            [JsonProperty("@experience")]
            public int experience { get; set; }

            [JsonProperty("@skill")]
            public int skill { get; set; }

            [JsonProperty("@loot")]
            public int loot { get; set; }

            [JsonProperty("@magic")]
            public int magic { get; set; }

            [JsonProperty("@spawn")]
            public int spawn { get; set; }
        }

        public class Map
        {
            [JsonProperty("@name")]
            public string name { get; set; }

            [JsonProperty("@author")]
            public string author { get; set; }

            [JsonProperty("@width")]
            public int width { get; set; }

            [JsonProperty("@height")]
            public int height { get; set; }
        }
    }
}
