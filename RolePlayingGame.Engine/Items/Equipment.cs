using RolePlayingGame.Engine.Items.Armor;

namespace RolePlayingGame.Engine.Items
{
    public class Equipment : IEquipment
    {
        private ArmorPiece _chestPiece;
        private ArmorPiece _helmet;
        private Weapon _weapon;

        public int Armor { get; private set; }

        public int Damage { get; private set; }

        public int WeaponSpeed => _weapon?.Speed ?? 0;

        public ChestPiece ChestPiece
        {
            get => _chestPiece as ChestPiece;
            set => SetArmor(value, ref _chestPiece);
        }

        public Helmet Helmet
        {
            get => _helmet as Helmet;
            set => SetArmor(value, ref _helmet);
        }

        public Weapon Weapon
        {
            get => _weapon;
            set
            {
                _weapon = value;
                Damage = value?.Damage ?? 0;
            }
        }

        private void SetArmor(ArmorPiece newArmor, ref ArmorPiece currentArmor)
        {
            Armor -= currentArmor?.Armor ?? 0;
            currentArmor = newArmor;
            Armor += newArmor?.Armor ?? 0;
        }
    }
}
