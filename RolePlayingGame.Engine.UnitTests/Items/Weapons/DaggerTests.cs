using FluentAssertions;
using RolePlayingGame.Engine.Items.Weapons;
using Xunit;

namespace RolePlayingGame.Engine.UnitTests.Items.Weapons
{
    public class DaggerTests
    {
        [Fact]
        public void Constructor_CreatesCorrectDagger()
        {
            var dagger = new Dagger("a", 1);

            dagger.Name.Should().Be("a");
            dagger.Damage.Should().Be(1);
            dagger.Speed.Should().Be(3);
        }
    }
}
