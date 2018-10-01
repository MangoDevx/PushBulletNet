using Newtonsoft.Json;

namespace PushBulletNet.PushBullet.Model
{
    public sealed class PushRequestModel
    {
        public PushRequestModel(string title, string content, string targetdeviceid)
        {
            Title = title;
            Content = content;
            TargetDeviceIdentity = TargetDeviceIdentity;
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Content { get; set; }

        [JsonProperty("type")]
        public string PushType { get; set; }

        [JsonProperty("device_iden")]
        public string TargetDeviceIdentity { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("source_device_iden")]
        public string SourceDeviceIdentity { get; set; }  

        [JsonProperty("client_iden")]
        public string ClientIdentity { get; set; }

        [JsonProperty("channel_tag")]
        public string ChannelTag { get; set; }
    }
}