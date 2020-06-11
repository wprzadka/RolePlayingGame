namespace RolePlayingGame.Engine.Items.Weapons
{
    public class Sword : Weapon
    {
        public Sword(string name, int damage) : base(name, damage)
        {
        }

        public override int Speed => 2;
    }
}