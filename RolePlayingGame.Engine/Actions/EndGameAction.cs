using System.Collections.Generic;
using RolePlayingGame.Engine.Exceptions;

namespace RolePlayingGame.Engine.Actions
{
    public class EndGameAction : IAction
    {
        public string Name => "End game.";

        public (string result, IList<IAction> possibleActions) Execute(IGameState gameState)
        {
            throw new EndGameException("The game has ended.");
        }
    }
}
