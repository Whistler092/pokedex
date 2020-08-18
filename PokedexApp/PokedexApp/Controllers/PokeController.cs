using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Pokedex.Core.Business.Interfaces;
using Pokedex.Entities;

namespace PokedexApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokeController : ControllerBase
    {
        private readonly IPokeBusiness pokeBusiness;
        private readonly IMemoryCache memoryCache;

        public PokeController(IPokeBusiness pokeBusiness, IMemoryCache memoryCache)
        {
            this.pokeBusiness = pokeBusiness;
            this.memoryCache = memoryCache;
        }

        // GET: api/<PokeController>
        [HttpGet]
        public async Task<ActionResult> Get(string offset)
        {
            return Ok(await pokeBusiness.GetAll(offset));
        }

        // GET api/<PokeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id, bool loadEvolutionChain)
        {
            Pokemon pokemon;
            var cacheKey = $"pokemon_{id}_{loadEvolutionChain}";
            if (!memoryCache.TryGetValue(cacheKey, out pokemon))
            {
                pokemon = await pokeBusiness.GetById(id, loadEvolutionChain);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(20));

                memoryCache.Set(cacheKey, pokemon, cacheEntryOptions);
            }
            return Ok(pokemon);
        }

        [HttpGet("byName")]
        public async Task<ActionResult> SearchPokemon(string search)
        {
            Pokemon pokemon = await pokeBusiness.SearchByName(search);

            return Ok(pokemon);
        }
    }

}
