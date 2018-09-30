using Newtonsoft.Json;

namespace PushBulletNet.POST
{
    public sealed class PushRequest : PostRequest
    {
        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("body")] public string Content { get; set; }
    }
}