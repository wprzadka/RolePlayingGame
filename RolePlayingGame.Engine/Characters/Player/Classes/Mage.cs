using RolePlayingGame.Engine.Items;

namespace RolePlayingGame.Engine.Characters.Player.Classes
{
    public class Mage : PlayerCharacter
    {
        public Mage(string name, IEquipment equipment) : base(name, equipment)
        {
            _healthPerLvl = 3;
            _damagePerLvl = 7;
        }

        public override int Health { get; protected set; } = 153;
        public override int BaseHealth { get; } = 150;
        protected override int BaseArmor { get; } = 1;
        protected override int BaseDamage { get; } = 40;

        protected override int ReduceIncomingDamage(int damage)
        {
            return damage;
        }
    }
}