namespace RolePlayingGame.Engine.Items
{
    public abstract class ArmorPiece : IDistinguishable
    {
        protected ArmorPiece(string name, int armor)
        {
            Name = name;
            Armor = armor;
        }

        public int Armor { get; }
        public string Name { get; }
    }
}