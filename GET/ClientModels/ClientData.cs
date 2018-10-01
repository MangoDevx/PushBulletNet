using System;
using Newtonsoft.Json;

namespace PushBulletNet.GET.ClientModels
{
    public sealed class ClientData
    {
        [JsonProperty("created")]
        public DateTimeOffset Created
        {
            get
            {
                if (double.TryParse(Created.ToString(), out var result))
                    return DateTimeOffset.FromUnixTimeSeconds((int)result);
                return Created;
            }
            set
            {

            }
        }

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
    }
}