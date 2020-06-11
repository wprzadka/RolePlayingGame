using FluentAssertions;
using NSubstitute;
using RolePlayingGame.Engine.Actions;
using RolePlayingGame.Engine.Exceptions;
using Xunit;

namespace RolePlayingGame.Engine.UnitTests.Actions
{
    public class EndGameActionTests
    {
        [Fact]
        public void Name_ReturnsCorrectValue()
        {
            var action = new EndGameAction();

            action.Name.Should().Be("End game.");
        }

        [Fact]
        public void Execute_ReturnsCorrectResult()
        {
            var action = new EndGameAction();

            action.Invoking(a => a.Execute(Substitute.For<IGameState>())).Should().Throw<EndGameException>();
        }
    }
}
