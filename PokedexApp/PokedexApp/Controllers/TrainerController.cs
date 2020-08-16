using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Core.Business.Interfaces;
using Pokedex.Entities;

namespace PokedexApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerBusiness trainerBusiness;

        public TrainerController(ITrainerBusiness trainerBusiness)
        {
            this.trainerBusiness = trainerBusiness;
        }


        // GET: api/<TrainerController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await trainerBusiness.GetAll());
        }

        // GET api/<TrainerController>/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await trainerBusiness.GetById(id));
        }

        // POST api/<TrainerController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Trainer trainer)
        {
            return Ok(await trainerBusiness.Save(trainer));
        }

        // PUT api/<TrainerController>/5
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] Trainer trainer)
        {
            return Ok(await trainerBusiness.Update(id, trainer));
        }
    }
}
