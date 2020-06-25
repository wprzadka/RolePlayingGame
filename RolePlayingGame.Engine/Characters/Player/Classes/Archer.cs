using RolePlayingGame.Engine.Items;

namespace RolePlayingGame.Engine.Characters.Player.Classes
{
    public class Archer : PlayerCharacter
    {
        public Archer(string name, IEquipment equipment) : base(name, equipment)
        {
            _armorPerLvl = 3;
            _healthPerLvl = 2;
            _damagePerLvl = 5;
        }

        public override int Health { get; protected set; } = 202;
        public override int BaseHealth { get; } = 200;
        protected override int BaseArmor { get; } = 10;
        protected override int BaseDamage { get; } = 20;

        protected override int ReduceIncomingDamage(int damage)
        {
            return damage;
        }
    }
}