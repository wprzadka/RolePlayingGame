namespace RolePlayingGame.Engine.Items.Weapons
{
    public class Dagger : Weapon
    {
        public Dagger(string name, int damage) : base(name, damage)
        {
        }

        public override int Speed => 3;
    }
}