using Pokedex.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokedex.Core.Business.Interfaces
{
    public interface IPokeBusiness
    {
        Task<GetAllQueryPagination> GetAll(string offset);
        Task<Pokemon> GetById(string id, bool loadEvolutionChain);
        Task<Pokemon> SearchByName(string search);
    }
}
