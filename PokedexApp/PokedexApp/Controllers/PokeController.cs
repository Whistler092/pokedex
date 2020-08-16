using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Core.Business.Interfaces;

namespace PokedexApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokeController : ControllerBase
    {
        private readonly IPokeBusiness pokeBusiness;

        public PokeController(IPokeBusiness pokeBusiness)
        {
            this.pokeBusiness = pokeBusiness;
        }

        // GET: api/<PokeController>
        [HttpGet]
        public async Task<ActionResult> Get(string offset)
        {
            return Ok(await pokeBusiness.GetAll(offset));
        }
    }
}
