using RolePlayingGame.Engine.Items.Armor;

namespace RolePlayingGame.Engine.Items
{
    public interface IEquipment
    {
        int Armor { get; }
        int Damage { get; }
        int WeaponSpeed { get; }
        ChestPiece ChestPiece { get; set; }
        Helmet Helmet { get; set; }
        Weapon Weapon { get; set; }
    }
}
