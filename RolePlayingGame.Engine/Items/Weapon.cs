namespace RolePlayingGame.Engine.Items
{
    public abstract class Weapon : IDistinguishable
    {
        protected Weapon(string name, int damage)
        {
            Name = name;
            Damage = damage;
        }

        public abstract int Speed { get; }
        public int Damage { get; }
        public string Name { get; }
    }
}