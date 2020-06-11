namespace RolePlayingGame.Engine.Characters.Player
{
    public interface IPlayerCharacter : IHumanoid
    {
        int Health { get; }
        int Armor { get; }
        int Damage { get; }
        int MaxHealth { get; }
        int Experience { get; set; }
        int Level { get; }

        int TakeDamage(int damage);
    }
}
