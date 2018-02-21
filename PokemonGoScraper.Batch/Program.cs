using System;
using System.Threading.Tasks;
using PokemonGoScraper.Infra.Hue;
using PokemonGoScraper.Infra.MapReader;

namespace PokemonGoScraper.Batch
{
    public static class Program
    {
        private static void Main()
        {
            var controller = new HueController();
            Task.Run(controller.Init).Wait();

            var mapRepository = new MapRepository(new Uri("https://leuven.pokehunt.me"));
            var hueBatch = new HueBatch(mapRepository, controller);
            Task.Run(hueBatch.Start).Wait();
        }
    }
}