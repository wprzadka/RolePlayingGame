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
    public class FleeActionTests
    {
        public FleeActionTests()
        {
            _actions = new List<IAction>();
            _dice = Substitute.For<IDice>();
            _fightLogic = Substitute.For<IFightLogic>();
            _gameState = Substitute.For<IGameState>();
            _gameState.Dice.Returns(_dice);
            _gameState.FightLogic.Returns(_fightLogic);
            _gameState.Zone.Actions.Returns(_actions);

            _enemy = Substitute.For<IKillable>();
            _enemy.ChanceToRun.Returns(50);
        }

        private readonly IList<IAction> _actions;
        private readonly IGameState _gameState;
        private readonly IDice _dice;
        private readonly IKillable _enemy;
        private readonly IFightLogic _fightLogic;

        [Fact]
        public void Name_ReturnsCorrectValue()
        {
            var action = new FleeAction(_enemy, _dice);

            action.Name.Should().Be("Flee!");
        }

        [Fact]
        public void Execute_IfPlayerDies_ThrowsEndGameException()
        {
            var player = Substitute.For<IPlayerCharacter>();
            _gameState.PlayerCharacter.Returns(player);
            _fightLogic.EnemyAttacksAndPlayerDies(_enemy, player, Arg.Any<StringBuilder>()).Returns(true);
            _dice.Roll(100).Returns(60);

            var action = new FleeAction(_enemy, _dice);

            action.Invoking(a => a.Execute(_gameState)).Should().Throw<EndGameException>().Which.Reason.Should()
                .Be(string.Empty);
        }

        [Fact]
        public void Execute_IfPlayerDoesNotManageToFlee_EnemyDoesNotDamage()
        {
            var player = Substitute.For<IPlayerCharacter>();
            player.Damage.Returns(10);
            _gameState.PlayerCharacter.Returns(player);
            _dice.Roll(100).Returns(60);

            var action = new FleeAction(_enemy, _dice);

            action.Execute(_gameState);
            _enemy.DidNotReceive().TakeDamage(10);
        }

        [Fact]
        public void Execute_IfPlayerDoesNotManageToFlee_PlayerTakesDamage()
        {
            var player = Substitute.For<IPlayerCharacter>();
            _gameState.PlayerCharacter.Returns(player);
            _dice.Roll(100).Returns(60);

            var action = new FleeAction(_enemy, _dice);

            action.Execute(_gameState);
            _fightLogic.Received(1).EnemyAttacksAndPlayerDies(_enemy, player, Arg.Any<StringBuilder>());
        }

        [Fact]
        public void Execute_IfPlayerDoesNotManageToFlee_ReturnsFightAndFleeActions()
        {
            _enemy.Name.Returns("b");
            var player = Substitute.For<IPlayerCharacter>();
            player.Name.Returns("a");
            _gameState.PlayerCharacter.Returns(player);
            _dice.Roll(100).Returns(60);
            var action = new FleeAction(_enemy, _dice);

            var (_, possibleActions) = action.Execute(_gameState);
            possibleActions.Should().HaveCount(2);
            possibleActions.Should().Contain(elem => elem is FightAction);
            possibleActions.Should().Contain(elem => elem is FleeAction);
        }

        [Fact]
        public void Execute_IfPlayerManagesToFlee_ReturnsLocationActions()
        {
            _dice.Roll(100).Returns(40);
            var action = new FleeAction(_enemy, _dice);

            action.Execute(_gameState).Should().Be(("You have managed to run!", _actions));
        }
    }
}