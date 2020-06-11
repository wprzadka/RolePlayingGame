using System;

namespace RolePlayingGame.Engine.Exceptions
{
    public class EnemyDiedException : Exception
    {
        public int DamageTaken { get; }

        public EnemyDiedException(int damageTaken)
        {
            DamageTaken = damageTaken;
        }
    }
}
