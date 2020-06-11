﻿using System.Collections.Generic;
using System.Linq;
using RolePlayingGame.Engine.Actions;
using RolePlayingGame.Engine.Characters.NonPlayer;

namespace RolePlayingGame.Engine.Zones
{
    public class TownZone : ZoneBase
    {
        private readonly IList<INonPlayerCharacter> _npcList;

        public TownZone(string name, string description, IEnumerable<IAction> actions,
            IList<INonPlayerCharacter> npcList) : base(name, description, actions)
        {
            _npcList = npcList;
        }

        protected override IEnumerable<IAction> AdditionalActions =>
            _npcList.SelectMany(npc => npc.Interactions.Append(npc.Attack));
    }
}