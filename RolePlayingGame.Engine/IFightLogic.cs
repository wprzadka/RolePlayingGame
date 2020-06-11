using System.Text;
using RolePlayingGame.Engine.Characters.NonPlayer;
using RolePlayingGame.Engine.Characters.Player;

namespace RolePlayingGame.Engine
{
    public interface IFightLogic
    {
        bool PlayerAttacksAndEnemyDies(IKillable enemy, IPlayerCharacter player, StringBuilder message);
        bool EnemyAttacksAndPlayerDies(IKillable enemy, IPlayerCharacter player, StringBuilder message);
    }
}
