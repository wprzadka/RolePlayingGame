using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using RolePlayingGame.Engine.Actions;
using RolePlayingGame.Engine.Zones;
using Xunit;

namespace RolePlayingGame.Engine.UnitTests.Actions
{
    public class TravelActionTests
    {
        public TravelActionTests()
        {
            _destinationActions = new List<IAction> {Substitute.For<IAction>()};
            _destination = Substitute.For<IZone>();
            _destination.Actions.Returns(_destinationActions);
            _destination.Name.Returns("destination");

            var currentZone = Substitute.For<IZone>();
            currentZone.Name.Returns("current");
            _gameState = Substitute.For<IGameState>();
            _gameState.Zone.Returns(currentZone);
        }

        private readonly IZone _destination;
        private readonly IGameState _gameState;
        private readonly IList<IAction> _destinationActions;

        [Fact]
        public void Name_ReturnsCorrectValue()
        {
            var action = new TravelAction(_destination);

            action.Name.Should().Be("Travel to destination.");
        }

        [Fact]
        public void Execute_ChangesZoneToTheDestination()
        {
            var action = new TravelAction(_destination);

            action.Execute(_gameState);

            _gameState.Zone.Should().Be(_destination);
        }

        [Fact]
        public void Execute_ReturnsCorrectActions()
        {
            var action = new TravelAction(_destination);

            var (_, actions) = action.Execute(_gameState);

            actions.Should().BeEquivalentTo(_destinationActions);
        }

        [Fact]
        public void Execute_ReturnsCorrectResult()
        {
            var action = new TravelAction(_destination);

            var (result, _) = action.Execute(_gameState);

            result.Should().Be("You have traveled from current to destination.");
        }
    }
}