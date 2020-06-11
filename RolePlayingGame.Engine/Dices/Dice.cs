using System;

namespace RolePlayingGame.Engine.Dices
{
    public class Dice : IDice
    {
        private readonly Random _random;

        public Dice(int seed)
        {
            _random = new Random(seed);
        }

        public int Roll(int k)
        {
            return _random.Next(1, k);
        }
    }
}
