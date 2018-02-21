namespace PokemonGoScraper.Domain
{
    public class IndividualValues
    {
        public int Attack { get; }
        public int Defense { get; }
        public int Stamina { get; }

        public IndividualValues(int attack, int defense, int stamina)
        {
            Attack = attack;
            Defense = defense;
            Stamina = stamina;
        }

        public double Perfection => (Attack + Defense + Stamina)/45.0;

        public override string ToString() => $"IVs: {Attack}/{Defense}/{Stamina} ({Perfection * 100.0:0.00}%)";
    }
}