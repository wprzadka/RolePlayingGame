namespace RolePlayingGame.Engine.Items.Weapons
{
    public class Staff : Weapon
    {
        public Staff(string name, int damage) : base(name, damage)
        {
        }

        public override int Speed => 1;
    }
}