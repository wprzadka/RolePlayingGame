using System.Collections.Generic;
using RolePlayingGame.Engine.Actions;
using RolePlayingGame.Engine.Items;

namespace RolePlayingGame.Engine.Characters.NonPlayer
{
    public class NonPlayerCharacter : Killable, INonPlayerCharacter
    {
        public NonPlayerCharacter(string name, int health, int damage, int armor, int experienceGain, int chanceToRun,
            IEquipment equipment, IList<IAction> interactions) :
            base(name, health, damage, armor, experienceGain, chanceToRun)
        {
            Equipment = equipment;
            Interactions = interactions;
        }

        public override int Damage => BaseDamage + Equipment.Damage;
        public override int Armor => BaseArmor + Equipment.Armor;

        public IEquipment Equipment { get; }

        public IList<IAction> Interactions { get; }
    }
}