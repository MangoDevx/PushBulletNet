using Newtonsoft.Json;

namespace PushBulletNet.PushBullet.Model
{
    public sealed class ErrorModel
    {
        [JsonProperty("cat")]
        public string Cat { get; set; }

        [JsonProperty("happy_to_see_you")]
        public bool Happy { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
