using System.Collections.Generic;

namespace PokemonGoScraper.Infra.MapReader
{
    internal class RootObject
    {
        public string lastpokemon { get; set; }
        public string lastpokestops { get; set; }
        public string oNeLat { get; set; }
        public string oNeLng { get; set; }
        public string oSwLat { get; set; }
        public string oSwLng { get; set; }
        public List<Pokemon> pokemons { get; set; }
        public List<object> pokestops { get; set; }
        public long timestamp { get; set; }
    }
}