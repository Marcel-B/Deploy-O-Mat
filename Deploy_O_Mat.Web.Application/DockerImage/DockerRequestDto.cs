using System.Collections.Generic;
using Newtonsoft.Json;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Images
{
    public class PushData
    {
        [JsonProperty("pushed_at")]
        public double PushedAt { get; set; }

        [JsonProperty("images")]
        public List<string> Images { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("pusher")]
        public string Pusher { get; set; }
    }

    public class Repository
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("is_trusted")]
        public bool IsTrusted { get; set; }

        [JsonProperty("full_description")]
        public string FullDescription { get; set; }

        [JsonProperty("repo_url")]
        public string RepoUrl { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("is_official")]
        public bool IsOfficial { get; set; }

        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("star_count")]
        public int StarsCount { get; set; }

        [JsonProperty("comment_count")]
        public int CommentCount { get; set; }

        [JsonProperty("date_created")]
        public double DateCreated { get; set; }

        [JsonProperty("repo_name")]
        public string RepoName { get; set; }

        [JsonProperty("dockerfile")]
        public string Dockerfile { get; set; }
    }
}
