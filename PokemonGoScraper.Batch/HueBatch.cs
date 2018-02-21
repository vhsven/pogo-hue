using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using PokemonGoScraper.DomainServices;

namespace PokemonGoScraper.Batch
{
    public class HueBatch
    {
        private readonly IMapRepository _mapRepository;
        private readonly IHueController _hueController;
        private readonly List<int> _wanted = new List<int> { 83, 113, 115, 128, 131, 143, 144, 145, 146, 150, 151, 58, 1, 4 };
        private const int SleepDuration = 60000;

        public HueBatch(IMapRepository mapRepository, IHueController hueController)
        {
            _mapRepository = mapRepository;
            _hueController = hueController;
        }

        public async Task Start()
        {
            while (true)
            {
                try
                {
                    await DoStuff();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private async Task DoStuff()
        {
            var spawns = await _mapRepository.GetData();
            var spawnList = spawns.ToList();

            Console.WriteLine($"Found {spawnList.Count} pokemon");

            var wanted = spawnList.Where(p => _wanted.Contains(p.Id));
            foreach (var pokemon in wanted)
            {
                Console.WriteLine($"(!) Found {pokemon}");
                var color = ColorTranslator.ToHtml(pokemon.Type1.Color);
                Console.WriteLine($"Type={pokemon.Type1.Type} => Color={color}");
                await _hueController.SetAllToColor(color.Substring(1));
                await Task.Delay(1000);
            }

            await Task.Delay(4000);

            await _hueController.SetAllToColor("000000");

            await Task.Delay(SleepDuration - 5000);
        }
    }
}