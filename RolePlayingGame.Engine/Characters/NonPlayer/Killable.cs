using RolePlayingGame.Engine.Actions;
using RolePlayingGame.Engine.Actions.Fight;
using RolePlayingGame.Engine.Exceptions;

namespace RolePlayingGame.Engine.Characters.NonPlayer
{
    public abstract class Killable : IKillable
    {
        protected Killable(string name, int health, int damage, int armor, int experienceGain, int chanceToRun)
        {
            Name = name;
            Health = health;
            BaseDamage = damage;
            BaseArmor = armor;
            ExperienceGain = experienceGain;
            ChanceToRun = chanceToRun;
        }

        protected int BaseDamage { get; }
        protected int BaseArmor { get; }

        public string Name { get; }
        public int Health { get; protected set; }
        public int ExperienceGain { get; }
        public abstract int Damage { get; }
        public abstract int Armor { get; }
        public int ChanceToRun { get; }

        public IAction Attack => new FightAction(this);

        public int TakeDamage(int damage)
        {
            var damageToTake = damage - Armor;
            if (damageToTake <= 0)
            {
                return 0;
            }

            Health -= damageToTake;

            if (Health <= 0)
            {
                throw new EnemyDiedException(damageToTake);
            }

            return damageToTake;
        }
    }
}