using RolePlayingGame.Engine.Characters.Player;
using RolePlayingGame.Engine.Dices;
using RolePlayingGame.Engine.Zones;

namespace RolePlayingGame.Engine
{
    public class GameState : IGameState
    {
        public GameState(IPlayerCharacter playerCharacter, IZone zone, IDice dice, IFightLogic fightLogic)
        {
            PlayerCharacter = playerCharacter;
            Zone = zone;
            Dice = dice;
            FightLogic = fightLogic;
            Message = "The game has sterted.";
        }

        public IPlayerCharacter PlayerCharacter { get; }
        public IZone Zone { get; set; }
        public IDice Dice { get; }
        public IFightLogic FightLogic { get; }

        public string Message { get; set; }
    }
}
