using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pokedex.Core.Integrations.Interfaces;
using Pokedex.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokedex.Core.Integrations
{
    public class PokeAPIIntegration : IPokeAPIIntegration
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public PokeAPIIntegration(IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        public async Task<List<Pokemon>> GetAll(string offset)
        {
            var baseUrl = configuration.GetValue<string>("APIIntegrations:Pokeapi");
            var apiUrl = $"{baseUrl}pokemon?limit=50&offset={offset}";

            var httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Clear();

            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();//ReadAsAsync<Pokemon>();
                var pokemonDto = JsonConvert.DeserializeObject<PokemonDto>(stringResult);

                return pokemonDto.results.Select(pokemon => new Pokemon
                {
                    Name = pokemon.name
                }).ToList();
            }
            return null;
        }

        public async Task<List<Pokemon>> GetPokemonDetail(string apiUrl)
        {
            var httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Clear();

            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();//ReadAsAsync<Pokemon>();
                var pokemonDto = JsonConvert.DeserializeObject<PokemonDto>(stringResult);

                return pokemonDto.results.Select(pokemon => new Pokemon
                {
                    Name = pokemon.name
                }).ToList();
            }
            return null;
        }
    }
}
