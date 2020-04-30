using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Deploy_O_Mat.Web.Domain.SignalR
{
    public class TransferData
    {
        [JsonPropertyName("id")]
          public string Id { get; set; }
        [JsonPropertyName("service")]
        public string Service { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("replicas")]
        public string Replicas { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
    }

    public class SocketDto
    {
        public SocketDto()
        {
            Values = new List<TransferData>();
        }
        [JsonPropertyName("values")]
        public List<TransferData> Values {get;set;}
    }
}