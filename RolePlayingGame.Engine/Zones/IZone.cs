using System;
using System.Collections.Generic;
using RolePlayingGame.Engine.Actions;

namespace RolePlayingGame.Engine.Zones
{
    public interface IZone : IDistinguishable
    {
        string Description { get; }

        Tuple<int, int> Position { get; }

        IList<IAction> Actions { get; }
    }
}
