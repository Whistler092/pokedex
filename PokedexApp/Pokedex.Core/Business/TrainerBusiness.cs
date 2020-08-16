using Pokedex.Core.Business.Interfaces;
using Pokedex.Core.Core.Interfaces;
using Pokedex.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokedex.Core.Business
{
    public class TrainerBusiness : ITrainerBusiness
    {
        private readonly ITrainerData trainerData;

        public TrainerBusiness(ITrainerData trainerData)
        {
            this.trainerData = trainerData;
        }

        public async Task<List<Trainer>> GetAll()
        {
            return await trainerData.GetAll();
        }

        public async Task<Trainer> GetById(Guid id)
        {
            return await trainerData.GetById(id);
        }

        public async Task<Trainer> Save(Trainer trainer)
        {
            return await trainerData.Save(trainer);
        }

        public async Task<Trainer> Update(Guid id, Trainer trainer)
        {
            return await trainerData.Update(id, trainer);
        }
    }
}
