using Pokedex.Core.Business.Interfaces;
using Pokedex.Core.Integrations.Interfaces;
using Pokedex.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokedex.Core.Business
{
    public class PokeBusiness : IPokeBusiness
    {
        private readonly IPokeAPIIntegration pokeAPIIntegration;

        public PokeBusiness(IPokeAPIIntegration pokeAPIIntegration)
        {
            this.pokeAPIIntegration = pokeAPIIntegration;
        }

        public async Task<List<Pokemon>> GetAll(string offset)
        {
           return await pokeAPIIntegration.GetAll(offset);
        }
    }
}
