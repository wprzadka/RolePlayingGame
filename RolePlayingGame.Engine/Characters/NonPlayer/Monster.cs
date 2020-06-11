namespace RolePlayingGame.Engine.Characters.NonPlayer
{
    public class Monster : Killable
    {
        public Monster(string name, int health, int damage, int armor, int experienceGain, int chanceToRun)
            : base(name, health, damage, armor, experienceGain, chanceToRun)
        {
        }

        public override int Damage => BaseDamage;
        public override int Armor => BaseArmor;
    }
}