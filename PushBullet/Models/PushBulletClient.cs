using System;
using Newtonsoft.Json;

namespace PushBulletNet.PushBullet.Models
{
    public sealed class PushBulletUser
    {
        [JsonProperty("created")]
        internal double Created { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("email_normalized")]
        public string EmailNormalized { get; set; }

        [JsonProperty("iden")]
        public string Iden { get; set; }

        [JsonProperty("image_url")]
        public Uri ImageUrl { get; set; }

        [JsonProperty("max_upload_size")]
        public long MaxUploadSize { get; set; }

        [JsonProperty("modified")]
        public double Modified { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public DateTimeOffset CreationDate { get; set; }
    }
}