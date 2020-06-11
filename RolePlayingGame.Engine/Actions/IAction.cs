using System.Collections.Generic;

namespace RolePlayingGame.Engine.Actions
{
    public interface IAction
    {
        string Name { get; }

        (string result, IList<IAction> possibleActions) Execute(IGameState gameState);
    }
}
