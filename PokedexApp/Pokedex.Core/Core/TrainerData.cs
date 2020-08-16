using Microsoft.EntityFrameworkCore;
using Pokedex.Core.Core.Interfaces;
using Pokedex.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokedex.Core.Core
{
    public class TrainerData : ITrainerData
    {
        private readonly PokedexContext context;

        public TrainerData(PokedexContext context)
        {
            this.context = context;
        }

        public async Task<List<Trainer>> GetAll()
        {
            return await context.Trainers.ToListAsync();
        }

        public async Task<Trainer> GetById(Guid id)
        {
            return await context.Trainers.FirstOrDefaultAsync(i => i.Id.Equals(id));
        }

        public async  Task<Trainer> Save(Trainer trainer)
        {
            trainer.Id = Guid.NewGuid();
            context.Trainers.Add(trainer);

            await context.SaveChangesAsync();
            return trainer;
        }

        public async Task<Trainer> Update(Guid id, Trainer trainer)
        {
            var savedTrain = await context.Trainers.FirstOrDefaultAsync(i => i.Id.Equals(id));
            savedTrain.Email = trainer.Email;
            savedTrain.FullName = trainer.FullName;

            if (savedTrain == null)
            {
                //return NotFound();
            }
            await context.SaveChangesAsync();
            return savedTrain;

        }
    }
}
