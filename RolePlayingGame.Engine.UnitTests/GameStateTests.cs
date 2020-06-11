using FluentAssertions;
using NSubstitute;
using RolePlayingGame.Engine.Characters.Player;
using RolePlayingGame.Engine.Dices;
using RolePlayingGame.Engine.Zones;
using Xunit;

namespace RolePlayingGame.Engine.UnitTests
{
    public class GameStateTests
    {
        [Fact]
        public void Constructor_CreatesCorrectGameState()
        {
            var player = Substitute.For<IPlayerCharacter>();
            var zone = Substitute.For<IZone>();
            var dice = Substitute.For<IDice>();
            var fightLogic = Substitute.For<IFightLogic>();

            var gameState = new GameState(player, zone, dice, fightLogic);

            gameState.PlayerCharacter.Should().Be(player);
            gameState.Zone.Should().Be(zone);
            gameState.Dice.Should().Be(dice);
            gameState.FightLogic.Should().Be(fightLogic);
        }
    }
}
