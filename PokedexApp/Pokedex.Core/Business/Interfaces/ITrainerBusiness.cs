using Pokedex.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokedex.Core.Business.Interfaces
{
    public interface ITrainerBusiness
    {
        Task<List<Trainer>> GetAll();

        Task<Trainer> Save(Trainer trainer);

        Task<Trainer> Update(Guid id, Trainer trainer);

        Task<Trainer> GetById(Guid id);

    }
}
