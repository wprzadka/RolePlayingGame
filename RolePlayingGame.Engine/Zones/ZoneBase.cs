using System;
using System.Collections.Generic;
using System.Linq;
using RolePlayingGame.Engine.Actions;

namespace RolePlayingGame.Engine.Zones
{
    public abstract class ZoneBase : IZone
    {
        private readonly IEnumerable<IAction> _baseActions;
        private readonly Lazy<IList<IAction>> _lazyActions;

        protected ZoneBase(string name, string description, Tuple<int, int> pos, IEnumerable<IAction> actions)
        {
            Name = name;
            Position = pos;

            Description = description;
            _baseActions = actions;

            _lazyActions = new Lazy<IList<IAction>>(ConcatenateActions);
        }

        protected abstract IEnumerable<IAction> AdditionalActions { get; }

        public string Name { get; }

        public string Description { get; }

        public Tuple<int, int> Position { get; }

        public IList<IZone> Neighbours { get; set; } = new List<IZone>();

        public IList<IAction> Actions => _lazyActions.Value;

        private IList<IAction> ConcatenateActions()
        {
            return _baseActions.Concat(AdditionalActions).ToList();
        }

        public void RemoveKilledActivities(IAction action)
        {
            _lazyActions.Value.Remove(action);
        }
    }
}