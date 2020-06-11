using System;

namespace RolePlayingGame.Engine.Exceptions
{
    public class EndGameException : Exception
    {
        public string Reason { get; }

        public EndGameException(string reason)
        {
            Reason = reason;
        }
    }
}
