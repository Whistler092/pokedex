using Pokedex.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokedex.Core.Integrations.Interfaces
{
    public interface IPokeAPIIntegration
    {
        Task<List<Pokemon>> GetAll(string offset);
    }
}
