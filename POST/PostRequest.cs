using Newtonsoft.Json;

namespace PushBulletNet.POST
{
    public class PostRequest
    {
        [JsonProperty("device_iden")] public string TargetDeviceIdentity { get; set; }

        [JsonProperty("email")] public string Email { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("source_device_iden")] public string SourceDeviceIdentity { get; set; }

        [JsonProperty("client_iden")] public string ClientIdentity { get; set; }

        [JsonProperty("channel_tag")] public string ChannelTag { get; set; }
    }
}