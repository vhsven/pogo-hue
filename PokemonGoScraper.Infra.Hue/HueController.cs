using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using PokemonGoScraper.Domain;
using PokemonGoScraper.DomainServices;
using Q42.HueApi;
using Q42.HueApi.ColorConverters;
using Q42.HueApi.ColorConverters.HSB;

namespace PokemonGoScraper.Infra.Hue
{
    public class HueController : IHueController
    {
        private LocalHueClient _client;

        public async Task Init()
        {
            var locator = new HttpBridgeLocator();
            var bridgeIPs = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(5));
            var bridgeIp = bridgeIPs.SingleOrDefault();

            if (bridgeIp == null) throw new Exception("Could not find Hue bridge");

            _client = new LocalHueClient(bridgeIp);
            //var appKey = await _client.RegisterAsync("PokemonGoScraper", "SurfacePro4");
            _client.Initialize("NFY1UwqUIXG2R0lk2QDT8jSlTkP9FjAa9hkOnoFE");
        }

        public async Task SetAllToColor(string hexColor)
        {
            try
            {
                var lightCommand = new LightCommand().TurnOn().SetColor(new RGBColor(hexColor));
                var hueResults = await _client.SendCommandAsync(lightCommand, new List<string> { "5", "6" });
            }
            catch (Exception e)
            {
                Console.WriteLine("Hue error: " + e);
            }
        }

        public async Task SetToColor(string hexColor)
        {
            try
            {
                var lightCommand = new LightCommand().TurnOn().SetColor(new RGBColor(hexColor));
                var lights = await _client.GetLightsAsync();
                var hueResults = await _client.SendCommandAsync(lightCommand, lights.Select(l => l.Id));
            }
            catch (Exception e)
            {
                Console.WriteLine("Hue error: " + e);
            }
        }

        public async Task SetStatus(ColoredType coloredType, double distance)
        {
            try
            {
                var typeColor = new RGBColor(ColorTranslator.ToHtml(coloredType.Color).Substring(1));
                var brightness = GetBrightness(distance); //TODO integrate brightness
                var lightCommand = new LightCommand().TurnOn().SetColor(typeColor);
                var lights = await _client.GetLightsAsync();
                var hueResults = await _client.SendCommandAsync(lightCommand, lights.Select(l => l.Id));
            }
            catch (Exception e)
            {
                Console.WriteLine("Hue error: " + e);
            }
        }

        private static byte GetBrightness(double distance) => (byte)((int)Math.Max(0, 2000 - distance)/2000*255);

    }
}
