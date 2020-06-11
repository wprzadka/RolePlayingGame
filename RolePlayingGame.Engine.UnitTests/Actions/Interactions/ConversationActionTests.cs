using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using RolePlayingGame.Engine.Actions;
using RolePlayingGame.Engine.Actions.Interactions;
using Xunit;

namespace RolePlayingGame.Engine.UnitTests.Actions.Interactions
{
    public class ConversationActionTests
    {
        public ConversationActionTests()
        {
            _gameState = Substitute.For<IGameState>();
            _gameState.Zone.Actions.Returns(new List<IAction> {Substitute.For<IAction>()});
        }

        private readonly IGameState _gameState;

        [Fact]
        public void Execute_ReturnsCorrectActions()
        {
            var responses = new List<IAction> {new ConversationAction("c", "d")};
            var action = new ConversationAction("a", "b", responses);

            var (_, actions) = action.Execute(_gameState);

            actions.Should().BeEquivalentTo(new List<IAction>
                {new ConversationAction("c", "d"), new ConversationAction("Bye!", "Bye!", _gameState.Zone.Actions)});
        }

        [Fact]
        public void Execute_ReturnsCorrectResult()
        {
            var action = new ConversationAction("a", "b", new List<IAction> {new ConversationAction("c", "d")});

            var (result, _) = action.Execute(_gameState);

            result.Should().Be("b");
        }

        [Fact]
        public void Name_ReturnsCorrectValue()
        {
            var action = new ConversationAction("a", "b");

            action.Name.Should().Be("a");
        }
    }
}