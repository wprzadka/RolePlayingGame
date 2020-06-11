using System.Collections.Generic;
using RolePlayingGame.Engine.Zones;

namespace RolePlayingGame.Engine.Actions
{
    public class TravelAction : Action
    {
        private readonly IZone _destination;

        public TravelAction(IZone destination) : base($"Travel to {destination.Name}.")
        {
            _destination = destination;
        }

        public override (string result, IList<IAction> possibleActions) Execute(IGameState gameState)
        {
            var currentZone = gameState.Zone;
            gameState.Zone = _destination;
            return ($"You have traveled from {currentZone.Name} to {_destination.Name}.", _destination.Actions);
        }
    }
}