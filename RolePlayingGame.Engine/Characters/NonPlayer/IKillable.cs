using RolePlayingGame.Engine.Actions;

namespace RolePlayingGame.Engine.Characters.NonPlayer
{
    public interface IKillable : IDistinguishable
    {
        int Health { get; }
        int ExperienceGain { get; }
        int Damage { get; }
        int Armor { get; }
        int ChanceToRun { get; }

        IAction Attack { get; }
        int TakeDamage(int damage);
    }
}