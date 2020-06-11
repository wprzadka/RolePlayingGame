using System;
using FluentAssertions;
using RolePlayingGame.Engine.Dices;
using Xunit;

namespace RolePlayingGame.Engine.UnitTests.Dices
{
    public class DiceTests
    {
        [Theory]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(8)]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(100)]
        public void Roll_ReturnsCorrectRandomValuesFromGivenDiceSize(int k)
        {
            const int seed = 0;
            var random = new Random(seed);
            var dice = new Dice(seed);

            dice.Roll(k).Should().Be(random.Next(1, k));
        }
    }
}
