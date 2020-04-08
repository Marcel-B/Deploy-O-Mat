using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace com.b_velop.Deploy_O_Mat.Application.Images
{
    public class PushData
    {
        [JsonPropertyName("pushed_at")]
        public double PushedAt { get; set; }

        [JsonPropertyName("images")]
        public List<string> Images { get; set; }

        [JsonPropertyName("tag")]
        public string Tag { get; set; }

        [JsonPropertyName("pusher")]
        public string Pusher { get; set; }
    }

    public class Repository
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("is_trusted")]
        public bool IsTrusted { get; set; }

        [JsonPropertyName("full_description")]
        public string FullDescription { get; set; }

        [JsonPropertyName("repo_url")]
        public string RepoUrl { get; set; }

        [JsonPropertyName("owner")]
        public string Owner { get; set; }

        [JsonPropertyName("is_official")]
        public bool IsOfficial { get; set; }

        [JsonPropertyName("is_private")]
        public bool IsPrivate { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("namespace")]
        public string Namespace { get; set; }

        [JsonPropertyName("star_count")]
        public int StarsCount { get; set; }

        [JsonPropertyName("comment_count")]
        public int CommentCount { get; set; }

        [JsonPropertyName("date_created")]
        public double DateCreated { get; set; }

        [JsonPropertyName("repo_name")]
        public string RepoName { get; set; }

        [JsonPropertyName("dockerfile")]
        public string Dockerfile { get; set; }
    }
}
