using RolePlayingGame.Engine.Actions.Fight;
using RolePlayingGame.Engine.Characters.Player;
using RolePlayingGame.Engine.Dices;
using RolePlayingGame.Engine.Zones;

namespace RolePlayingGame.Engine
{
    public interface IGameState
    {
        IPlayerCharacter PlayerCharacter { get; }

        IZone Zone { get; set; }

        IDice Dice { get; }

        IFightLogic FightLogic { get; }

        string Message { get; set; }
    }
}
