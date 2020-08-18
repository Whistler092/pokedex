using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pokedex.Core.Integrations.Interfaces;
using Pokedex.Entities;
using System;
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

        public async Task<GetAllQueryPagination> GetAll(string offset)
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
                    if (item != null)
                    {
                        string id = item.url.Split('/')[item.url.Split('/').Length - 2];
                        retList.Add(new Pokemon
                        {
                            Id = System.Convert.ToInt32(id),
                            Name = item.name,
                        });

                    }
                }
                return new GetAllQueryPagination
                {
                    count = pokemonDto.count,
                    next = pokemonDto.next,
                    previous = pokemonDto.previous,
                    results = retList
                };
            }
            return null;
        }

        public async Task<Pokemon> GetById(string id, bool loadEvolutionChain)
        {
            var url = $"https://pokeapi.co/api/v2/pokemon/{id}/";

            var pokemonDetail = await GetPokemonDetail(url);
            if (pokemonDetail != null)
            {
                List<string> evolutionChain = loadEvolutionChain ? await getEvolutionChain(id) : null;
                return new Pokemon
                {
                    Id = pokemonDetail.id,
                    Name = pokemonDetail.name,
                    Photo = pokemonDetail.sprites.front_default,
                    Types = pokemonDetail.types.Select(i => i.type.name).ToList(),
                    Height = pokemonDetail.height.ToString(),
                    Weight = pokemonDetail.weight.ToString(),
                    Moves = pokemonDetail.moves.Select(i => i.move.name).ToList(),
                    EvolutionChain = evolutionChain
                };
            }

            return null;
        }

        private async Task<List<string>> getEvolutionChain(string id)
        {
            List<string> evolves = new List<string>();
            var url = $"https://pokeapi.co/api/v2/pokemon-species/{id}/";

            var httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Clear();

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();//ReadAsAsync<Pokemon>();
                var pokemonDto = JsonConvert.DeserializeObject<PokemonSpeciesDto>(stringResult);

                //if (pokemonDto != null && pokemonDto.evolves_from_species != null)
                //{
                //    evolves.Add(pokemonDto.evolves_from_species.name);
                //}

                if (pokemonDto != null && pokemonDto.evolution_chain != null)
                {
                    httpClient = httpClientFactory.CreateClient();
                    httpClient.DefaultRequestHeaders.Clear();

                    response = await httpClient.GetAsync(pokemonDto.evolution_chain.url);

                    if (response.IsSuccessStatusCode)
                    {
                        stringResult = await response.Content.ReadAsStringAsync();
                        var evolutionChainDto = JsonConvert.DeserializeObject<EvolutionChainDto>(stringResult);
                        if(evolutionChainDto != null && evolutionChainDto.chain != null)
                        {
                            evolves.AddRange(getEvolutionChainToString(evolutionChainDto.chain));
                        }

                    }
                }
            }
            return evolves.Distinct().ToList();
        }

        private IEnumerable<string> getEvolutionChainToString(EvolvesTo evolutionChainDto)
        {
            List<string> evolution = new List<string>();

            if(evolutionChainDto != null && evolutionChainDto.species != null)
            {
                evolution.Add(evolutionChainDto.species.name);
            }
            if(evolutionChainDto != null && evolutionChainDto.evolves_to != null && evolutionChainDto.evolves_to.Any())
            {
                foreach (var item in evolutionChainDto.evolves_to)
                {
                    evolution.AddRange(getEvolutionChainToString(item));
                }
            }

            return evolution;
        }

        public async Task<PokemonDto> GetPokemonDetail(string apiUrl)
        {
            var httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Clear();

            var response = await httpClient.GetAsync(apiUrl.ToLower());

            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();//ReadAsAsync<Pokemon>();
                var pokemonDto = JsonConvert.DeserializeObject<PokemonDto>(stringResult);

                return pokemonDto;
            }
            return null;
        }

        public async Task<Pokemon> SearchByName(string search)
        {
            var url = $"https://pokeapi.co/api/v2/pokemon/{search}/";

            var pokemonDetail = await GetPokemonDetail(url);

            if (pokemonDetail != null)
            {
                return new Pokemon
                {
                    Id = pokemonDetail.id,
                };
            }

            return null;
        }
    }
}
