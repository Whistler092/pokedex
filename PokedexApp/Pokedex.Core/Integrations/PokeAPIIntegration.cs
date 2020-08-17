using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pokedex.Core.Integrations.Interfaces;
using Pokedex.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication.ExtendedProtection;
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
                var pokemonDto = JsonConvert.DeserializeObject<PokemonQueryDto>(stringResult);

                List<Pokemon> retList = new List<Pokemon>();
                foreach (var item in pokemonDto.results)
                {
                    var pokemonDetail = await GetPokemonDetail(item.url);
                    if (pokemonDetail != null)
                    {
                        retList.Add(new Pokemon
                        {
                            Id = pokemonDetail.id,
                            Name = pokemonDetail.name,
                            Photo = pokemonDetail.sprites.front_default,
                            Types = pokemonDetail.types.Select(i => i.type.name).ToList(),
                            Height = pokemonDetail.height.ToString(),
                            Weight = pokemonDetail.weight.ToString(),
                            Moves = pokemonDetail.moves.Select(i => i.move.name).ToList()
                        });
                    }
                }

                return retList;
                //return pokemonDto.results.Select(pokemon => new Pokemon
                //{
                //    Name = pokemon.name
                //}).ToList();
            }
            return null;
        }

        public async Task<PokemonDto> GetPokemonDetail(string apiUrl)
        {
            var httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Clear();

            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();//ReadAsAsync<Pokemon>();
                var pokemonDto = JsonConvert.DeserializeObject<PokemonDto>(stringResult);

                return pokemonDto;
            }
            return null;
        }
    }
}
