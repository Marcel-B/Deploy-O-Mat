using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Domain;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Deploy_O_Mat.Service.Services
{
    public class DockerImageService : IDockerImageService
    {
        private HttpClient _httpClient;
        private ILogger<DockerImageService> _logger;

        public DockerImageService(
            HttpClient httpClient,
            ILogger<DockerImageService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<DockerImage>> GetDockerImages()
        {
            try
            {
                var result = await _httpClient.GetAsync("https://deploy.qaybe.de/api/dockerimage");
                var content = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<DockerImage>>(content);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while request DockerImages");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while request DockerImages");

            }
            return new DockerImage[0];
        }
    }
}
