using System.Collections.Generic;
using System.Text;
using RolePlayingGame.Engine.Characters.NonPlayer;
using RolePlayingGame.Engine.Dices;
using RolePlayingGame.Engine.Exceptions;

namespace RolePlayingGame.Engine.Actions.Fight
{
    public class FleeAction : Action
    {
        private const int DiceSize = 100;

        private readonly IKillable _enemy;
        private readonly IDice _dice;

        public FleeAction(IKillable enemy, IDice dice) : base("Flee!")
        {
            _enemy = enemy;
            _dice = dice;
        }

        public override (string result, IList<IAction> possibleActions) Execute(IGameState gameState)
        {
            var roll = _dice.Roll(DiceSize);

            if (roll <= _enemy.ChanceToRun)
            {
                return ("You have managed to run!", gameState.Zone.Actions);
            }

            var message = new StringBuilder();
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