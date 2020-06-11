using System;

namespace RolePlayingGame.Engine.Exceptions
{
    public class YouDiedException : Exception
    {
        public int DamageTaken { get; }

        public YouDiedException(int damageTaken)
        {
            DamageTaken = damageTaken;
        }
    }
}
