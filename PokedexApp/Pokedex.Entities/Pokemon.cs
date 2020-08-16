using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Entities
{
    public class Pokemon
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Photo { get; set; }

        public List<string> Types { get; set; }

        public string Height { get; set; }

        public string Weight { get; set; }

        public List<string> Moves { get; set; }

        public List<string> EvolutionChain { get; set; }
    }
}
