using FluentAssertions;
using RolePlayingGame.Engine.Items.Weapons;
using Xunit;

namespace RolePlayingGame.Engine.UnitTests.Items.Weapons
{
    public class SwordTests
    {
        [Fact]
        public void Constructor_CreatesCorrectSword()
        {
            var sword = new Sword("a", 10);

            sword.Name.Should().Be("a");
            sword.Damage.Should().Be(10);
            sword.Speed.Should().Be(2);
        }
    }
}