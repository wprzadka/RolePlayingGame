using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NSubstitute;
using RolePlayingGame.Engine.Actions;
using RolePlayingGame.Engine.Actions.Fight;
using RolePlayingGame.Engine.Characters.NonPlayer;
using RolePlayingGame.Engine.Characters.Player;
using RolePlayingGame.Engine.Dices;
using RolePlayingGame.Engine.Exceptions;
using Xunit;

namespace RolePlayingGame.Engine.UnitTests.Actions.Fight
{
    public class FightActionTests
    {
        public FightActionTests()
        {
            _enemy = Substitute.For<IKillable>();
            _enemy.Name.Returns("someone");
            _fightLogic = Substitute.For<IFightLogic>();
            _gameState = Substitute.For<IGameState>();
            _gameState.FightLogic.Returns(_fightLogic);
            _gameState.Zone.Actions.Returns(new List<IAction> {Substitute.For<IAction>()});
            _gameState.PlayerCharacter.Returns(Substitute.For<IPlayerCharacter>());
        }

        private readonly IGameState _gameState;
        private readonly IKillable _enemy;
        private readonly IFightLogic _fightLogic;

        [Fact]
        public void Execute_IfEnemyDies_GoesBackToLocation()
        {
            _fightLogic.PlayerAttacksAndEnemyDies(_enemy, _gameState.PlayerCharacter, Arg.Any<StringBuilder>())
                .Returns(true);
            var action = new FightAction(_enemy);

            var (_, actions) = action.Execute(_gameState);

            actions.Should().BeEquivalentTo(_gameState.Zone.Actions);
        }

        [Fact]
        public void Execute_IfEnemyDies_PlayerGainsExperience()
        {
            _enemy.ExperienceGain.Returns(5);
            _fightLogic.PlayerAttacksAndEnemyDies(_enemy, _gameState.PlayerCharacter, Arg.Any<StringBuilder>())
                .Returns(true);
            var action = new FightAction(_enemy);

            action.Execute(_gameState);

            _gameState.PlayerCharacter.Received().Experience = 5;
        }

        [Fact]
        public void Execute_IfNobodyDies_ContinuesTheFight()
        {
            var dice = Substitute.For<IDice>();
            _gameState.Dice.Returns(dice);
            var action = new FightAction(_enemy);

            var (_, actions) = action.Execute(_gameState);

            actions.Should().BeEquivalentTo(new List<IAction>
            {
                new FightAction(_enemy),
                new FleeAction(_enemy, dice)
            });
        }

        [Fact]
        public void Execute_IfPlayerDies_ThrowsException()
        {
            _fightLogic.EnemyAttacksAndPlayerDies(_enemy, _gameState.PlayerCharacter, Arg.Any<StringBuilder>())
                .Returns(true);
            var action = new FightAction(_enemy);

            action.Invoking(a => a.Execute(_gameState)).Should().Throw<EndGameException>().Which.Reason.Should()
                .Be(string.Empty);
        }

        [Fact]
        public void Name_ReturnsCorrectValue()
        {
            var action = new FightAction(_enemy);

            action.Name.Should().Be("Fight with someone.");
        }
    }
}