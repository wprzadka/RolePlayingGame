using FluentAssertions;
using RolePlayingGame.Engine.Items;
using RolePlayingGame.Engine.Items.Armor;
using RolePlayingGame.Engine.Items.Weapons;
using Xunit;

namespace RolePlayingGame.Engine.UnitTests.Items
{
    public class EquipmentTests
    {
        [Fact]
        public void Armor_ForEmptyArmorSlot_ReturnsCorrectValue()
        {
            var helmet = new Helmet("a", 10);

            var equipment = new Equipment
            {
                Helmet = helmet
            };

            equipment.Armor.Should().Be(10);
        }

        [Fact]
        public void Armor_ReturnsSumOfArmorValues()
        {
            var helmet = new Helmet("a", 1);
            var chest = new ChestPiece("a", 2);

            var equipment = new Equipment
            {
                Helmet = helmet,
                ChestPiece = chest
            };

            equipment.Armor.Should().Be(3);
        }

        [Fact]
        public void Armor_WhenChangingGear_ReturnsCorrectValue()
        {
            var helmet = new Helmet("a", 1);
            var chest = new ChestPiece("a", 2);

            var equipment = new Equipment
            {
                Helmet = helmet,
                ChestPiece = chest
            };

            equipment.Helmet = new Helmet("a", 2);

            equipment.Armor.Should().Be(4);
        }

        [Fact]
        public void Armor_WhenUnequippingGear_ReturnsCorrectValue()
        {
            var helmet = new Helmet("a", 1);
            var chest = new ChestPiece("a", 2);

            var equipment = new Equipment
            {
                Helmet = helmet,
                ChestPiece = chest
            };

            equipment.Helmet = null;

            equipment.Armor.Should().Be(2);
        }

        [Fact]
        public void Damage_ForEmptyWeaponSlot_ReturnsCorrectValue()
        {
            var equipment = new Equipment();

            equipment.Damage.Should().Be(0);
        }

        [Fact]
        public void Damage_ReturnsCorrectValue()
        {
            var weapon = new Dagger("a", 10);

            var equipment = new Equipment
            {
                Weapon = weapon
            };

            equipment.Damage.Should().Be(10);
        }

        [Fact]
        public void Damage_WhenUnequippingGear_ReturnsZero()
        {
            var weapon = new Dagger("1", 10);

            var equipment = new Equipment
            {
                Weapon = weapon
            };

            equipment.Weapon = null;

            equipment.Damage.Should().Be(0);
        }

        [Fact]
        public void WeaponSpeed_ForEmptyWeaponSlot_ReturnsZero()
        {
            var equipment = new Equipment();

            equipment.WeaponSpeed.Should().Be(0);
        }

        [Fact]
        public void WeaponSpeed_ReturnsEquippedWeaponSpeed()
        {
            var weapon = new Dagger("a", 10);
            var equipment = new Equipment
            {
                Weapon = weapon
            };

            equipment.WeaponSpeed.Should().Be(3);
        }

        [Fact]
        public void GetWeapon_ReturnsCorrectItem()
        {
            var weapon = new Sword("a", 10);
            var equipment = new Equipment {Weapon = weapon};

            equipment.Weapon.Should().Be(weapon);
        }

        [Fact]
        public void GetHelmet_ReturnsCorrectItem()
        {
            var helmet = new Helmet("a", 10);
            var equipment = new Equipment { Helmet = helmet };

            equipment.Helmet.Should().Be(helmet);
        }

        [Fact]
        public void GetChestPiece_ReturnsCorrectItem()
        {
            var chestPiece = new ChestPiece("a", 10);
            var equipment = new Equipment { ChestPiece = chestPiece };

            equipment.ChestPiece.Should().Be(chestPiece);
        }
    }
}