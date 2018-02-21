using System.Collections.Generic;
using System.Threading.Tasks;
using PokemonGoScraper.Domain;

namespace PokemonGoScraper.DomainServices
{
    public interface IMapRepository
    {
        Task<IEnumerable<PokemonSpawn>> GetData();
    }
}