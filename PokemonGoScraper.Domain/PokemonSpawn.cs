using System;
using System.Device.Location;

namespace PokemonGoScraper.Domain
{
    public class PokemonSpawn
    {
        private static readonly GeoCoordinate Home = new GeoCoordinate(50.8868427, 4.68541);

        public DateTime DisappearTime { get; }
        public string EncounterId { get; }
        public IndividualValues IndividualValues { get; }
        public DateTime LastModified { get; }
        public GeoCoordinate GeoCoordinate { get; }
        public int? Move1 { get; }
        public int? Move2 { get; }
        public int Id { get; }
        public string Name { get; }
        public Rarity Rarity { get; }
        public ColoredType Type1 { get; }
        public ColoredType Type2 { get; }
        public string SpawnPointId { get; }

        public PokemonSpawn(DateTime disappearTime, string encounterId, IndividualValues individualValues, DateTime lastModified, GeoCoordinate geoCoordinate, int? move1, int? move2, int id, string name, Rarity rarity, ColoredType type1, ColoredType type2, string spawnPointId)
        {
            DisappearTime = disappearTime;
            EncounterId = encounterId;
            IndividualValues = individualValues;
            LastModified = lastModified;
            GeoCoordinate = geoCoordinate;
            Move1 = move1;
            Move2 = move2;
            Id = id;
            Name = name;
            Rarity = rarity;
            Type1 = type1;
            Type2 = type2;
            SpawnPointId = spawnPointId;
        }

        public override string ToString() => $"#{Id:000} {Name} @ {GeoCoordinate.GetDistanceTo(Home):0}m | {IndividualValues}";
    }
}