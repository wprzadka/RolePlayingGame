using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using RolePlayingGame.Engine.Actions;
using RolePlayingGame.Engine.Characters.NonPlayer;
using RolePlayingGame.Engine.Zones;
using Xunit;

namespace RolePlayingGame.Engine.UnitTests.Zone
{
    public class WildZoneTests
    {
        [Fact]
        public void Actions_ReturnsBothZoneAndAttackEnemiesActions()
        {
            var zoneAction = Substitute.For<IAction>();
            var zoneActions = new List<IAction> { zoneAction };
            var npc1FightAction = Substitute.For<IAction>();
            var npc2FightAction = Substitute.For<IAction>();

            var npc1 = Substitute.For<INonPlayerCharacter>();
            npc1.Attack.Returns(npc1FightAction);

            var npc2 = Substitute.For<INonPlayerCharacter>();
            npc2.Attack.Returns(npc2FightAction);

            var enemiesList = new List<INonPlayerCharacter>
            {
                npc1,
                npc2
            };

            var zone = new WildZone("my zone", "some description", new System.Tuple<int, int>(0, 0), zoneActions, enemiesList);

            zone.Actions.Should().HaveCount(3);
            zone.Actions.Should().ContainEquivalentOf(zoneAction);
            zone.Actions.Should().ContainEquivalentOf(npc1FightAction);
            zone.Actions.Should().ContainEquivalentOf(npc2FightAction);
        }

        [Fact]
        public void Constructor_CreatesCorrectWildZone()
        {
            var zone = new WildZone("forest", "some description", new System.Tuple<int, int>(0, 0), new List<IAction> { Substitute.For<IAction>() },
                new List<IKillable> { Substitute.For<IKillable>() });

            zone.Name.Should().Be("forest");
            zone.Description.Should().Be("some description");
        }
    }
}
