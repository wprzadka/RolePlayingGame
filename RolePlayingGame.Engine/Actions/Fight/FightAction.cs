using System.Collections.Generic;
using System.Text;
using RolePlayingGame.Engine.Characters.NonPlayer;
using RolePlayingGame.Engine.Exceptions;

namespace RolePlayingGame.Engine.Actions.Fight
{
    public class FightAction : Action
    {
        private readonly IKillable _enemy;

        public FightAction(IKillable enemy) : base($"Fight with {enemy.Name}.")
        {
            _enemy = enemy;
        }

        public override (string result, IList<IAction> possibleActions) Execute(IGameState gameState)
        {
            var message = new StringBuilder();

            if (gameState.FightLogic.PlayerAttacksAndEnemyDies(_enemy, gameState.PlayerCharacter, message))
            {
                gameState.PlayerCharacter.Experience += _enemy.ExperienceGain;
                gameState.Zone.RemoveKilledActivities(this);
                return (message.ToString(), gameState.Zone.Actions);
            }

            if (gameState.FightLogic.EnemyAttacksAndPlayerDies(_enemy, gameState.PlayerCharacter, message))
            {
                throw new EndGameException(message.ToString());
            }

            return (message.ToString(), new List<IAction>
            {
                new FightAction(_enemy),
                new FleeAction(_enemy, gameState.Dice)
            });
        }
    }
}
