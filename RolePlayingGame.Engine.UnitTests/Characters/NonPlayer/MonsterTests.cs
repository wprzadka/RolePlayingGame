using FluentAssertions;
using RolePlayingGame.Engine.Actions.Fight;
using RolePlayingGame.Engine.Characters.NonPlayer;
using RolePlayingGame.Engine.Exceptions;
using Xunit;

namespace RolePlayingGame.Engine.UnitTests.Characters.NonPlayer
{
    public class MonsterTests
    {
        private readonly Monster _monster;

        public MonsterTests()
        {
            _monster = new Monster("a", 10, 2, 3, 4, 5);
        }

        [Fact]
        public void Constructor_CreatesCorrectMonster()
        {
            _monster.Name.Should().Be("a");
            _monster.Health.Should().Be(10);
            _monster.Damage.Should().Be(2);
            _monster.Armor.Should().Be(3);
            _monster.ExperienceGain.Should().Be(4);
            _monster.ChanceToRun.Should().Be(5);
        }

        [Fact]
        public void Attack_ReturnsFightAction()
        {
            _monster.Attack.Should().BeEquivalentTo(new FightAction(_monster));
        }

        [Fact]
        public void TakeDamage_IfDamageIsGreaterThanArmor_DecreasesHealth()
        {
            var health = _monster.Health;

            _monster.TakeDamage(5);

            _monster.Health.Should().BeLessThan(health);
        }

        [Fact]
        public void TakeDamage_IfDamageIsEqualToArmor_DoesNotDecreaseHealth()
        {
            var health = _monster.Health;

            _monster.TakeDamage(3);

            _monster.Health.Should().Be(health);
        }

        [Fact]
        public void TakeDamage_IfDamageIsLessThanArmor_DoesNotDecreaseHealth()
        {
            var health = _monster.Health;

            _monster.TakeDamage(2);

            _monster.Health.Should().Be(health);
        }

        [Fact]
        public void TakeDamage_CorrectlyDecreasesHealth()
        {
            _monster.TakeDamage(8);

            _monster.Health.Should().Be(5);
        }

        [Fact]
        public void TakeDamage_IfEnemyDies_ThrowsException()
        {

            _monster.Invoking(m => m.TakeDamage(100)).Should().Throw<EnemyDiedException>().Which.DamageTaken.Should()
                .Be(97);
        }
    }
}
