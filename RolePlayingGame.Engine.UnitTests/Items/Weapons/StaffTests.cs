using FluentAssertions;
using RolePlayingGame.Engine.Items.Weapons;
using Xunit;

namespace RolePlayingGame.Engine.UnitTests.Items.Weapons
{
    public class StaffTests
    {
        [Fact]
        public void ConstructorCreatesCorrectStaff()
        {
            var staff = new Staff("b", 20);

            staff.Name.Should().Be("b");
            staff.Damage.Should().Be(20);
            staff.Speed.Should().Be(1);
        }
    }
}