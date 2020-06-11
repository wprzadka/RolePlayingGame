using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using RolePlayingGame.Engine.Actions;
using RolePlayingGame.Engine.Actions.Fight;
using RolePlayingGame.Engine.Characters.NonPlayer;
using RolePlayingGame.Engine.Exceptions;
using RolePlayingGame.Engine.Items;
using Xunit;

namespace RolePlayingGame.Engine.UnitTests.Characters.NonPlayer
{
    public class NonPlayerCharacterTests
    {
        public NonPlayerCharacterTests()
        {
            _equipment = Substitute.For<IEquipment>();
            _actions = new List<IAction> {Substitute.For<IAction>()};
            _npc = new NonPlayerCharacter("a", 10, 2, 3, 4, 5, _equipment, _actions);
        }

        private readonly NonPlayerCharacter _npc;
        private readonly IEquipment _equipment;
        private readonly IList<IAction> _actions;

        [Fact]
        public void Armor_ReturnsSumOfBaseArmorAndEquipmentArmor()
        {
            _equipment.Armor.Returns(5);

            _npc.Armor.Should().Be(8);
        }

        [Fact]
        public void Damage_ReturnsSumOfBaseDamageAndEquipmentDamage()
        {
            _equipment.Damage.Returns(5);

            _npc.Damage.Should().Be(7);
        }

        [Fact]
        public void Attack_ReturnsFightAction()
        {
            _npc.Attack.Should().BeEquivalentTo(new FightAction(_npc));
        }

        [Fact]
        public void Constructor_CreatesCorrectMonster()
        {
            _npc.Name.Should().Be("a");
            _npc.Health.Should().Be(10);
            _npc.ExperienceGain.Should().Be(4);
            _npc.ChanceToRun.Should().Be(5);
            _npc.Equipment.Should().Be(_equipment);
            _npc.Interactions.Should().BeEquivalentTo(_actions);
        }

        [Fact]
        public void TakeDamage_CorrectlyDecreasesHealth()
        {
            _npc.TakeDamage(8);

            _npc.Health.Should().Be(5);
        }

        [Fact]
        public void TakeDamage_IfDamageIsEqualToArmor_DoesNotDecreaseHealth()
        {
            var health = _npc.Health;

            _npc.TakeDamage(3);

            _npc.Health.Should().Be(health);
        }

        [Fact]
        public void TakeDamage_IfDamageIsGreaterThanArmor_DecreasesHealth()
        {
            var health = _npc.Health;

            _npc.TakeDamage(5);

            _npc.Health.Should().BeLessThan(health);
        }

        [Fact]
        public void TakeDamage_IfDamageIsLessThanArmor_DoesNotDecreaseHealth()
        {
            var health = _npc.Health;

            _npc.TakeDamage(2);

            _npc.Health.Should().Be(health);
        }

        [Fact]
        public void TakeDamage_IfEnemyDies_ThrowsException()
        {
            _npc.Invoking(m => m.TakeDamage(100)).Should().Throw<EnemyDiedException>().Which.DamageTaken.Should()
                .Be(97);
        }
    }
}