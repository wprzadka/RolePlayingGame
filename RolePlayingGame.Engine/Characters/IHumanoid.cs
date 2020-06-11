using RolePlayingGame.Engine.Items;

namespace RolePlayingGame.Engine.Characters
{
    public interface IHumanoid : IDistinguishable
    {
        IEquipment Equipment { get; }
    }
}
