using System.Collections.Generic;

namespace Pokedex.Entities
{
    public class GetAllQueryPagination
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }

        public List<Pokemon> results { get; set; }

    }

    public class PokemonQueryDto
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

    public class PokemonDto
    {
        //public List<Ability> abilities { get; set; }
        public int base_experience { get; set; }
        //public List<Form> forms { get; set; }
        //public List<GameIndice> game_indices { get; set; }
        public int height { get; set; }
        public List<object> held_items { get; set; }
        public int id { get; set; }
        public bool is_default { get; set; }
        public string location_area_encounters { get; set; }
        public List<MoveDto> moves { get; set; }
        public string name { get; set; }
        public int order { get; set; }
        public SpeciesDto species { get; set; }
        public SpritesDto sprites { get; set; }
        //public List<Stat> stats { get; set; }
        public List<TypeDto> types { get; set; }
        public int weight { get; set; }
    }

    public class MoveDto
    {
        public Move2Dto move { get; set; }
    }

    public class Move2Dto
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class SpeciesDto
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class SpritesDto
    {
        public string back_default { get; set; }
        public object back_female { get; set; }
        public string back_shiny { get; set; }
        public object back_shiny_female { get; set; }
        public string front_default { get; set; }
        public object front_female { get; set; }
        public string front_shiny { get; set; }
        public object front_shiny_female { get; set; }
        //public Other other { get; set; }
        //public Versions versions { get; set; }
    }

    public class TypeDto2
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class TypeDto
    {
        public int slot { get; set; }
        public TypeDto2 type { get; set; }
    }

    public class PokemonSpeciesDto
    {
        public EvolutionChainUrlDto evolution_chain { get; set; }

        public EvolvesFromDto evolves_from_species { get; set; }

    }

    public class EvolvesFromDto
    {
        public string name { get; set; }
    }

    public class EvolutionChainUrlDto
    {
        public string url { get; set; }
    }

    public class EvolutionChainDto
    {
        public EvolvesTo chain { get; set; }
        public int id { get; set; }
    }


    public class Species
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class EvolvesTo
    {
        public List<EvolvesTo> evolves_to { get; set; }
        public Species species { get; set; }
    }

}
