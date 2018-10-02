using Newtonsoft.Json;

namespace PushBulletNet.PushBullet.Model
{
    public sealed class PushBulletDeviceData
    {
        [JsonProperty("devices")]
        public PushBulletDevice[] Devices { get; set; }
    }

    public sealed class PushBulletDevice
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