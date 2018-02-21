using System.Collections.Generic;

namespace PokemonGoScraper.Infra.MapReader
{
    internal class Pokemon
    {
        public long disappear_time { get; set; }
        public string encounter_id { get; set; }
        public int? individual_attack { get; set; }
        public int? individual_defense { get; set; }
        public int? individual_stamina { get; set; }
        public long last_modified { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int? move_1 { get; set; }
        public int? move_2 { get; set; }
        public int pokemon_id { get; set; }
        public string pokemon_name { get; set; }
        public string pokemon_rarity { get; set; }
        public List<PokemonType> pokemon_types { get; set; }
        public string spawnpoint_id { get; set; }
    }
}