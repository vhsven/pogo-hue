using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PokemonGoScraper.Domain;
using PokemonGoScraper.DomainServices;
using Type = PokemonGoScraper.Domain.Type;

namespace PokemonGoScraper.Infra.MapReader
{
    public class MapRepository : IMapRepository
    {
        private readonly Uri _host;
        private long? _lastTimestamp;

        public MapRepository(Uri host)
        {
            _host = host;
        }

        public async Task<IEnumerable<PokemonSpawn>> GetData()
        {
            var data = await GetRawData();
            return data?.pokemons?.Select(ParseData) ?? Enumerable.Empty<PokemonSpawn>();
        }

        private async Task<RootObject> GetRawData()
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                var target = CreateUrl();
                var response = await client.GetAsync(target);
                var body = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RootObject>(body);
                _lastTimestamp = result.timestamp;
                return result;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return null;
            }
        }

        private Uri CreateUrl()
        {
            var appendix = "raw_data?pokemon=true&lastpokemon=true&pokestops=false&gyms=false&scanned=false&spawnpoints=false&swLat=50.82516959655247&swLng=4.535722678125012&neLat=50.934453926418804&neLng=4.865312521875012&oSwLat=50.82516959655247&oSwLng=4.535722678125012&oNeLat=50.934453926418804&oNeLng=4.865312521875012";
            if (_lastTimestamp.HasValue)
            {
                appendix += "&timestamp=" + _lastTimestamp.Value;
            }
            return new Uri(_host, appendix);
        }

        private static PokemonSpawn ParseData(Pokemon pokemon) =>
            new PokemonSpawn(
                ToDateTime(pokemon.disappear_time),
                pokemon.encounter_id,
                pokemon.individual_attack.HasValue && pokemon.individual_defense.HasValue && pokemon.individual_stamina.HasValue
                    ? new IndividualValues(pokemon.individual_attack.Value, pokemon.individual_defense.Value, pokemon.individual_stamina.Value)
                    : null,
                ToDateTime(pokemon.last_modified),
                new GeoCoordinate(pokemon.latitude, pokemon.longitude),
                pokemon.move_1,
                pokemon.move_2,
                pokemon.pokemon_id,
                pokemon.pokemon_name,
                Rarity.Common,
                ToType(pokemon.pokemon_types.First()),
                pokemon.pokemon_types.Count == 2
                    ? ToType(pokemon.pokemon_types.First())
                    : new ColoredType(Type.None, Color.Transparent),
                pokemon.spawnpoint_id
            );

        private static DateTime ToDateTime(long unixTimeStamp) => new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(unixTimeStamp);

        private static ColoredType ToType(PokemonType type) => new ColoredType((Type)Enum.Parse(typeof(Type), type.type), type.color);
    }
}