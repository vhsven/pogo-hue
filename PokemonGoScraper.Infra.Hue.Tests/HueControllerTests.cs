using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PokemonGoScraper.Infra.Hue.Tests
{
    [TestClass]
    public class HueControllerTests
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var hueController = new HueController();
            await hueController.Init();
            await hueController.SetToColor("FFFFFF");
        }
    }
}
