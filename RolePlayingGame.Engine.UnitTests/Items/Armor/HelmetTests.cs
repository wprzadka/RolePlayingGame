using FluentAssertions;
using RolePlayingGame.Engine.Items.Armor;
using Xunit;

namespace RolePlayingGame.Engine.UnitTests.Items.Armor
{
    public class HelmetTests
    {
        [Fact]
        public void Constructor_ReturnsCorrectHelmet()
        {
            var helmet = new Helmet("g", 4);

            helmet.Name.Should().Be("g");
            helmet.Armor.Should().Be(4);
        }
    }
}
