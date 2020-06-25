using RolePlayingGame.Engine.Items;

namespace RolePlayingGame.Engine.Characters.Player.Classes
{
    public class Warrior : PlayerCharacter
    {
        public Warrior(string name, IEquipment equipment) : base(name, equipment)
        {
            _healthPerLvl = 7;
            _armorPerLvl = 3;
        }

        public override int Health { get; protected set; } = 207;
        public override int BaseHealth { get; } = 200;
        protected override int BaseArmor { get; } = 50;
        protected override int BaseDamage { get; } = 10;

        protected override int ReduceIncomingDamage(int damage)
        {
            return damage;
        }
    }
}