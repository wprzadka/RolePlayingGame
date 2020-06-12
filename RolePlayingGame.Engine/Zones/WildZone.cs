using System;
using System.Collections.Generic;
using System.Linq;
using RolePlayingGame.Engine.Actions;
using RolePlayingGame.Engine.Characters.NonPlayer;

namespace RolePlayingGame.Engine.Zones
{
    public class WildZone : ZoneBase
    {
        private readonly IEnumerable<IKillable> _enemies;

        public WildZone(string name, string description, Tuple<int, int> pos, IEnumerable<IAction> actions, IEnumerable<IKillable> enemies) : base(name, description, pos, actions)
        {
            _enemies = enemies;
        }

        protected override IEnumerable<IAction> AdditionalActions => _enemies.Select(enemy => enemy.Attack);
    }
}
