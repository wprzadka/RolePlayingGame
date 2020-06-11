using System;
using System.Text;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using RolePlayingGame.Engine.Characters.NonPlayer;
using RolePlayingGame.Engine.Characters.Player;
using RolePlayingGame.Engine.Exceptions;
using Xunit;

namespace RolePlayingGame.Engine.UnitTests
{
    public class FightLogicTests
    {
        public FightLogicTests()
        {
            _enemy = Substitute.For<IKillable>();
            _enemy.Name.Returns("Big Bad Enemy");
            _player = Substitute.For<IPlayerCharacter>();
            _player.Name.Returns("Hero");
        }

        private readonly IKillable _enemy;
        private readonly IPlayerCharacter _player;

        [Fact]
        public void EnemyAttacksAndPlayerDies_IfPlayerDies_CreatesCorrectMessage()
        {
            _enemy.Damage.Returns(10);
            _player.TakeDamage(10).Throws(new YouDiedException(10));
            var fightLogic = new FightLogic();
            var builder = new StringBuilder();

            fightLogic.EnemyAttacksAndPlayerDies(_enemy, _player, builder);
            builder.ToString().Should().Be($"Big Bad Enemy deals 10 damage to Hero.{Environment.NewLine}" +
                                           $"Hero dies. Game over.{Environment.NewLine}");
        }

        [Fact]
        public void EnemyAttacksAndPlayerDies_IfPlayerDies_ReturnsTrue()
        {
            _enemy.Damage.Returns(10);
            _player.TakeDamage(10).Throws(new YouDiedException(10));
            var fightLogic = new FightLogic();

            fightLogic.EnemyAttacksAndPlayerDies(_enemy, _player, new StringBuilder()).Should().BeTrue();
        }

        [Fact]
        public void EnemyAttacksAndPlayerDies_IfPlayerDoesNotDie_CreatesCorrectMessage()
        {
            var fightLogic = new FightLogic();
            var builder = new StringBuilder();

            fightLogic.EnemyAttacksAndPlayerDies(_enemy, _player, builder);
            builder.ToString().Should().Be($"Big Bad Enemy deals no damage to Hero.{Environment.NewLine}");
        }

        [Fact]
        public void EnemyAttacksAndPlayerDies_IfPlayerDoesNotDie_ReturnsFalse()
        {
            var fightLogic = new FightLogic();

            fightLogic.EnemyAttacksAndPlayerDies(_enemy, _player, new StringBuilder()).Should().BeFalse();
        }

        [Fact]
        public void EnemyAttacksAndPlayerDies_PlayerTakesDamage()
        {
            var enemy = Substitute.For<IKillable>();
            enemy.Damage.Returns(10);
            var player = Substitute.For<IPlayerCharacter>();
            var fightLogic = new FightLogic();

            fightLogic.EnemyAttacksAndPlayerDies(enemy, player, new StringBuilder());
            player.Received(1).TakeDamage(10);
        }

        [Fact]
        public void PlayerAttacksAndEnemyDies_EnemyTakesDamage()
        {
            _player.Damage.Returns(10);
            var fightLogic = new FightLogic();

            fightLogic.PlayerAttacksAndEnemyDies(_enemy, _player, new StringBuilder());
            _enemy.Received(1).TakeDamage(10);
        }

        [Fact]
        public void PlayerAttacksAndEnemyDies_IfEnemyDies_CreatesCorrectMessage()
        {
            _enemy.TakeDamage(10).Throws(new EnemyDiedException(10));
            _player.Damage.Returns(10);
            var fightLogic = new FightLogic();
            var builder = new StringBuilder();

            fightLogic.PlayerAttacksAndEnemyDies(_enemy, _player, builder);
            builder.ToString().Should().Be($"Hero deals 10 damage to Big Bad Enemy.{Environment.NewLine}" +
                                           $"Big Bad Enemy dies.{Environment.NewLine}");
        }

        [Fact]
        public void PlayerAttacksAndEnemyDies_IfEnemyDies_ReturnsTrue()
        {
            _enemy.TakeDamage(10).Throws(new EnemyDiedException(10));
            _player.Damage.Returns(10);
            var fightLogic = new FightLogic();

            fightLogic.PlayerAttacksAndEnemyDies(_enemy, _player, new StringBuilder()).Should().BeTrue();
        }

        [Fact]
        public void PlayerAttacksAndEnemyDies_IfEnemyDoesNotDie_CreatesCorrectMessage()
        {
            var fightLogic = new FightLogic();
            var builder = new StringBuilder();

            fightLogic.PlayerAttacksAndEnemyDies(_enemy, _player, builder);
            builder.ToString().Should().Be($"Hero deals no damage to Big Bad Enemy.{Environment.NewLine}");
        }

        [Fact]
        public void PlayerAttacksAndEnemyDies_IfEnemyDoesNotDie_ReturnsFalse()
        {
            var fightLogic = new FightLogic();

            fightLogic.PlayerAttacksAndEnemyDies(_enemy, _player, new StringBuilder()).Should().BeFalse();
        }
    }
}