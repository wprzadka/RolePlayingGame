using System.Collections.Generic;
using RolePlayingGame.Engine.Actions;

namespace RolePlayingGame.Engine.Zones
{
    public interface IZone : IDistinguishable
    {
        string Description { get; }

        IList<IAction> Actions { get; }
    }
}
