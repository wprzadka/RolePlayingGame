using System.Collections.Generic;

namespace RolePlayingGame.Engine.Actions
{
    public abstract class Action : IAction
    {
        protected Action(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public abstract (string result, IList<IAction> possibleActions) Execute(IGameState gameState);
    }
}
