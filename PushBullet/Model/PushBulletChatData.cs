using Newtonsoft.Json;
using System;

namespace PushBulletNet.PushBullet.Model
{
    public sealed class PushBulletChatData
    {
        [JsonProperty("chats")]
        public PushBulletChat[] Chats { get; set; }
    }

    public sealed class PushBulletChat
    {
        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("created")]
        public double Created { get; set; }

        [JsonProperty("iden")]
        public string Iden { get; set; }

        [JsonProperty("modified")]
        public double Modified { get; set; }

        [JsonProperty("with")]
        public PushBulletWithChat With { get; set; }
    }

    public sealed class PushBulletWithChat
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("email_normalized")]
        public string EmailNormalized { get; set; }

        [JsonProperty("iden")]
        public string Iden { get; set; }

        [JsonProperty("image_url")]
        public Uri ImageUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

}
