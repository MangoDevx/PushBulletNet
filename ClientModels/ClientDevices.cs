using Newtonsoft.Json;

namespace PushBulletNet.Client
{
    public class ClientDevices
    {
        [JsonProperty("devices")]
        public Device[] Devices { get; set; }
    }

    public partial class Device
    {
        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("app_version")]
        public long AppVersion { get; set; }

        [JsonProperty("created")]
        public double Created { get; set; }

        [JsonProperty("iden")]
        public string Iden { get; set; }

        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("modified")]
        public double Modified { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("push_token")]
        public string PushToken { get; set; }
    }
}
