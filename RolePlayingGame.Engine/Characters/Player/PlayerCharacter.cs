using RolePlayingGame.Engine.Exceptions;
using RolePlayingGame.Engine.Items;

namespace RolePlayingGame.Engine.Characters.Player
{
    public abstract class PlayerCharacter : IPlayerCharacter
    {
        private int _experience;

        protected int _healthPerLvl = 1;

        protected int _armorPerLvl = 1;

        protected int _damagePerLvl = 1;


        protected PlayerCharacter(string name, IEquipment equipment)
        {
            Name = name;
            Equipment = equipment;
        }

        private int ExperienceToNextLevel => 100 * Level * (1 + Level / 10);
        public abstract int Health { get; protected set; }
        protected abstract int BaseArmor { get; }
        public int Armor => BaseArmor + Equipment.Armor + Level * _armorPerLvl;
        protected abstract int BaseDamage { get; }
        public int Damage => BaseDamage + Equipment.Damage + Level * _damagePerLvl;
        public string Name { get; }
        public abstract int BaseHealth { get; }
        public int MaxHealth => BaseHealth + Level * _healthPerLvl;
        public int Experience
        {
            get => _experience;
            set
            {
                _experience = value;
                if (Experience > ExperienceToNextLevel)
                {
                    Level += 1;
                    Health = MaxHealth;
                    _experience = 0;
                }
            }
        }

        public int Level { get; private set; } = 1;

        public int TakeDamage(int damage)
        {
            var damageToTake = damage - Armor;
            damageToTake = ReduceIncomingDamage(damageToTake);

            if (damageToTake <= 0)
            {
                return 0;
            }

            Health -= damageToTake;

            if (Health <= 0)
            {
                throw new YouDiedException(damageToTake);
            }

            return damageToTake;
        }

        public IEquipment Equipment { get; }

        protected abstract int ReduceIncomingDamage(int damage);
    }
}