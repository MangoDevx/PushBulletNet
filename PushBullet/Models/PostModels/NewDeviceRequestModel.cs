using Newtonsoft.Json;

namespace PushBulletNet.PushBullet.Models.PostModels
{
    public sealed class NewDeviceModel
    {
        public NewDeviceModel()
        {

        }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("push_token")]
        public string Token { get; set; }

        [JsonProperty("app_version")]
        public int AppVersion { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("has_sms")]
        public string HasSms { get; set; }
    }
}
