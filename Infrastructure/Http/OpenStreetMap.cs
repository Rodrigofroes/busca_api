using System.Runtime.CompilerServices;
using BackAppPersonal.Domain.Entities;
using BackAppPersonal.Domain.Intefaces;
using Newtonsoft.Json;

namespace BackAppPersonal.Infrastructure.Http
{
    public class OpenStreetMap : IOpenStreetMap
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public OpenStreetMap(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
            _client.BaseAddress = new Uri(_configuration["OpenStreetMap:BaseAddress"]);
            _client.DefaultRequestHeaders.Add("User-Agent", "BackAppPersonal");
        }

        public async Task<List<OpenStreetMapResponse>> ConsultarLogradouro(string logradouro)
        {
            var response = await _client.GetAsync($"search?format=json&q={logradouro}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<OpenStreetMapResponse>>(content);
        }
    }
}
