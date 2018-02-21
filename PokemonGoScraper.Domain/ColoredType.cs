using System.Drawing;

namespace PokemonGoScraper.Domain
{
    public class ColoredType
    {
        public Type Type { get; }
        public Color Color { get; }

        public ColoredType(Type type, Color color)
        {
            Type = type;
            Color = color;
        }

        public ColoredType(Type type, string hexColor)
            : this(type, ColorTranslator.FromHtml(hexColor)) { }

        public override string ToString()
        {
            return $"[{ColorTranslator.ToHtml(Color)}]{Type}";
        }
    }
}