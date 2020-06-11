using FluentAssertions;
using RolePlayingGame.Engine.Items.Armor;
using Xunit;

namespace RolePlayingGame.Engine.UnitTests.Items.Armor
{
    public class ChestPieceTests
    {
        [Fact]
        public void Constructor_CreatesCorrectChestPiece()
        {
            var chestPiece = new ChestPiece("c", 10);

            chestPiece.Name.Should().Be("c");
            chestPiece.Armor.Should().Be(10);
        }
    }
}
