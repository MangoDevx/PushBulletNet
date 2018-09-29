using Newtonsoft.Json;

namespace PushBulletNet.GET.ClientModels
{
    public class ClientPushes
    {
        [JsonProperty("pushes")] public Push[] Pushes { get; set; }
    }

    public class Push
    {
        [JsonProperty("active")] public bool Active { get; set; }

        [JsonProperty("body")] public string Body { get; set; }

        [JsonProperty("created")] public double Created { get; set; }

        [JsonProperty("direction")] public string Direction { get; set; }

        [JsonProperty("dismissed")] public bool Dismissed { get; set; }

        [JsonProperty("iden")] public string Iden { get; set; }

        [JsonProperty("modified")] public double Modified { get; set; }

        [JsonProperty("receiver_email")] public string ReceiverEmail { get; set; }

        [JsonProperty("receiver_email_normalized")]
        public string ReceiverEmailNormalized { get; set; }

        [JsonProperty("receiver_iden")] public string ReceiverIden { get; set; }

        [JsonProperty("sender_email")] public string SenderEmail { get; set; }

        [JsonProperty("sender_email_normalized")]
        public string SenderEmailNormalized { get; set; }

        [JsonProperty("sender_iden")] public string SenderIden { get; set; }

        [JsonProperty("sender_name")] public string SenderName { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("type")] public string Type { get; set; }
    }
}