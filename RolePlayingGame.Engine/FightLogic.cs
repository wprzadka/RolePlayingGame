using System.Text;
using RolePlayingGame.Engine.Characters.NonPlayer;
using RolePlayingGame.Engine.Characters.Player;
using RolePlayingGame.Engine.Exceptions;

namespace RolePlayingGame.Engine
{
    public class FightLogic : IFightLogic
    {
        public bool PlayerAttacksAndEnemyDies(IKillable enemy, IPlayerCharacter player, StringBuilder message)
        {
            try
            {
                var damageToEnemy = enemy.TakeDamage(player.Damage);
                message.AppendLine(damageToEnemy <= 0
                    ? $"{player.Name} deals no damage to {enemy.Name}."
                    : $"{player.Name} deals {damageToEnemy} damage to {enemy.Name}.");
            }
            catch (EnemyDiedException enemyDied)
            {
                message.AppendLine($"{player.Name} deals {enemyDied.DamageTaken} damage to {enemy.Name}.");
                message.AppendLine($"{enemy.Name} dies.");

                return true;
            }

            return false;
        }

        public bool EnemyAttacksAndPlayerDies(IKillable enemy, IPlayerCharacter player, StringBuilder message)
        {
            try
            {
                var damageToPlayer = player.TakeDamage(enemy.Damage);
                message.AppendLine(damageToPlayer <= 0
                    ? $"{enemy.Name} deals no damage to {player.Name}."
                    : $"{enemy.Name} deals {damageToPlayer} damage to {player.Name}.");
            }
            catch (YouDiedException youDied)
            {
                message.AppendLine($"{enemy.Name} deals {youDied.DamageTaken} damage to {player.Name}.");
                message.AppendLine($"{player.Name} dies. Game over.");

                return true;
            }

            return false;
        }
    }
}
