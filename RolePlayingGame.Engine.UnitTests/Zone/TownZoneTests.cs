using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using RolePlayingGame.Engine.Actions;
using RolePlayingGame.Engine.Characters.NonPlayer;
using RolePlayingGame.Engine.Zones;
using Xunit;

namespace RolePlayingGame.Engine.UnitTests.Zone
{
    public class TownZoneTests
    {
        [Fact]
        public void Actions_ReturnsBothZoneAndNpcActions()
        {
            var zoneAction = Substitute.For<IAction>();
            var zoneActions = new List<IAction> {zoneAction};
            var npc1Action1 = Substitute.For<IAction>();
            var npc1Action2 = Substitute.For<IAction>();
            var npc1Actions = new List<IAction> {npc1Action1, npc1Action1};
            var npc1FightAction = Substitute.For<IAction>();
            var npc2Action = Substitute.For<IAction>();
            var npc2Actions = new List<IAction> {npc2Action};
            var npc2FightAction = Substitute.For<IAction>();

            var npc1 = Substitute.For<INonPlayerCharacter>();
            npc1.Interactions.Returns(npc1Actions);
            npc1.Attack.Returns(npc1FightAction);

            var npc2 = Substitute.For<INonPlayerCharacter>();
            npc2.Interactions.Returns(npc2Actions);
            npc2.Attack.Returns(npc2FightAction);

            var npcList = new List<INonPlayerCharacter>
            {
                npc1,
                npc2
            };

            var zone = new TownZone("my zone", "some description", new System.Tuple<int, int>(0, 0), zoneActions, npcList);

            zone.Actions.Should().HaveCount(6);
            zone.Actions.Should().ContainEquivalentOf(zoneAction);
            zone.Actions.Should().ContainEquivalentOf(npc1Action1);
            zone.Actions.Should().ContainEquivalentOf(npc1Action2);
            zone.Actions.Should().ContainEquivalentOf(npc1FightAction);
            zone.Actions.Should().ContainEquivalentOf(npc2Action);
            zone.Actions.Should().ContainEquivalentOf(npc2FightAction);
        }

        [Fact]
        public void Constructor_CreatesCorrectTownZone()
        {
            var zone = new TownZone("my town", "some description", new System.Tuple<int, int>(0, 0), new List<IAction> {Substitute.For<IAction>()},
                new List<INonPlayerCharacter> {Substitute.For<INonPlayerCharacter>()});

            zone.Name.Should().Be("my town");
            zone.Description.Should().Be("some description");
        }
    }
}