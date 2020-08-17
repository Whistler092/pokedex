using Pokedex.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokedex.Core.Business.Interfaces
{
    public interface IPokeBusiness
    {
        Task<List<Pokemon>> GetAll(string offset);
        Task<Pokemon> GetById(string id);
    }
}
