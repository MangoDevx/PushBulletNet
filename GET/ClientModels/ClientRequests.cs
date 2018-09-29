using System;
using Newtonsoft.Json;

namespace PushBulletNet.GET.ClientModels
{
    public class ClientRequests
    {
        [JsonProperty("data")] public Data Content { get; set; }

        [JsonProperty("file_name")] public string FileName { get; set; }

        [JsonProperty("file_type")] public string FileType { get; set; }

        [JsonProperty("file_url")] public Uri FileUrl { get; set; }

        [JsonProperty("upload_url")] public Uri UploadUrl { get; set; }

        public class Data
        {
            [JsonProperty("acl")] public string Acl { get; set; }

            [JsonProperty("awsaccesskeyid")] public string Awsaccesskeyid { get; set; }

            [JsonProperty("content-type")] public string ContentType { get; set; }

            [JsonProperty("key")] public string Key { get; set; }

            [JsonProperty("policy")] public string Policy { get; set; }

            [JsonProperty("signature")] public string Signature { get; set; }
        }
    }
}