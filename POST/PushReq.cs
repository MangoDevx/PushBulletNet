using Newtonsoft.Json;

namespace PushBulletNet.POST
{
    public class PushReq : PostReq
    {
        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("body")] public string Content { get; set; }
    }
}