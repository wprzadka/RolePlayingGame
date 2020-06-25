using RolePlayingGame.Engine.Items;

namespace RolePlayingGame.Engine.Characters.Player.Classes
{
    public class Mage : PlayerCharacter
    {
        public Mage(string name, IEquipment equipment) : base(name, equipment)
        {
        }

        public override int Health { get; protected set; } = 150;
        public override int MaxHealth { get; } = 150;
        protected override int BaseArmor { get; } = 1;
        protected override int BaseDamage { get; } = 40;

        protected override int ReduceIncomingDamage(int damage)
        {
            return damage;
        }
    }
}