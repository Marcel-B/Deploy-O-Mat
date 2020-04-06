using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Domain;
using Newtonsoft.Json;

namespace Deploy_O_Mat.Service.Services
{
    public class DockerImageService : IDockerImageService
    {
        private HttpClient _httpClient;

        public DockerImageService(
            HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<DockerImage>> GetDockerImages()
        {
            var result = await _httpClient.GetAsync("https://deploy.qaybe.de/api/dockerimage");
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<DockerImage>>(content);
        }
    }
}
