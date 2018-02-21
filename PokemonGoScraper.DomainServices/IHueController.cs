using System.Threading.Tasks;

namespace PokemonGoScraper.DomainServices
{
    public interface IHueController
    {
        Task Init();
        Task SetAllToColor(string hexColor);
    }
}