using System;
using System.Collections.Generic;
using System.Linq;
using RolePlayingGame.Engine.Actions;

namespace RolePlayingGame.Engine.Zones
{
    public abstract class ZoneBase : IZone
    {
        private readonly IEnumerable<IAction> _baseActions;

        protected ZoneBase(string name, string description, Tuple<int, int> pos, IEnumerable<IAction> actions)
        {
            Name = name;
            Position = pos;

            Description = description;
            _baseActions = actions;
        }

        protected abstract IEnumerable<IAction> AdditionalActions { get; }

        public string Name { get; }

        public string Description { get; }

        public Tuple<int, int> Position { get; }

        public IList<IZone> Neighbours { get; set; } = new List<IZone>();

        public IList<IAction> Actions
        {
            get
            {
                var allActions = _baseActions.ToList();
                allActions.AddRange(AdditionalActions);
                allActions.AddRange(Neighbours.Select(n => new TravelAction(n)));

                return allActions;
            }
        }
    }
}