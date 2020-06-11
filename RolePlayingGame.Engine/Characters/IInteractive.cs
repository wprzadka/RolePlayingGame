using System.Collections.Generic;
using RolePlayingGame.Engine.Actions;

namespace RolePlayingGame.Engine.Characters
{
    public interface IInteractive : IDistinguishable
    {
        IList<IAction> Interactions { get; }
    }
}
