using System;
using Newtonsoft.Json;


namespace PushBulletNet
{
    public class ClientObject
    {
        internal ClientObject()
        {
        }

        [JsonProperty("created")]
        public double Created { get; internal set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("email_normalized")]
        public string EmailNormalized { get; internal set; }

        [JsonProperty("iden")]
        public string Iden { get; internal set; }

        [JsonProperty("image_url")]
        public Uri ImageUrl { get; internal set; }

        [JsonProperty("max_upload_size")]
        public long MaxUploadSize { get; internal set; }

        [JsonProperty("modified")]
        public double Modified { get; internal set; }

        [JsonProperty("name")]
        public string Name { get; internal set; }
    }
}
