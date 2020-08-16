using System.Collections.Generic;

namespace Pokedex.Entities
{
    public class PokemonDto
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<PokemonResultsDto> results { get; set; }
    }

    public class PokemonResultsDto
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}
