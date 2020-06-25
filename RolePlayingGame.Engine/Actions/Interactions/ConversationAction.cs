using System.Collections.Generic;

namespace RolePlayingGame.Engine.Actions.Interactions
{
    public class ConversationAction : Action
    {
        private readonly string _result;
        private readonly IList<IAction> _responses = new List<IAction>();

        public ConversationAction(string name, string result, IList<IAction> responses) : this(name, result)
        {
            _responses = responses;
        }

        public ConversationAction(string name, string result) : base(name)
        {
            _result = result;
        }

        public override (string result, IList<IAction> possibleActions) Execute(IGameState gameState)
        {
            if (_responses.Count < 1)
            {
                _responses.Add(new ConversationAction("Bye!", "Bye!", gameState.Zone.Actions));
            }
            return (_result, _responses);
        }
    }
}
