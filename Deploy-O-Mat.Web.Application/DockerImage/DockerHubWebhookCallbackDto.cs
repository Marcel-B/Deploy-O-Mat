using System.Text.Json.Serialization;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Images
{
    public class DockerHubWebhookCallbackDto
    {
        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("context")]
        public string Context { get; set; }

        [JsonPropertyName("target_url")]
        public string TargetUrl { get; set; }
    }
}
