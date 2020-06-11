using RolePlayingGame.Engine.Items;

namespace RolePlayingGame.Engine.Characters.Player.Classes
{
    public class Warrior : PlayerCharacter
    {
        public Warrior(string name, IEquipment equipment) : base(name, equipment)
        {
        }

        public override int Health { get; protected set; } = 200;
        public override int MaxHealth { get; } = 200;
        protected override int BaseArmor { get; } = 10;
        protected override int BaseDamage { get; } = 10;

        protected override int ReduceIncomingDamage(int damage)
        {
            return damage;
        }
    }
}