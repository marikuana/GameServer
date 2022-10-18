using System.Numerics;

namespace GameServerCore
{
    public class RandomPosition : IPosition
    {
        private readonly Random _random;
        private float _range;
        public RandomPosition(float range = 10f)
        {
            _range = range;
            _random = new Random();
        }

        public Vector3 Pos
        {
            get
            {
                return new Vector3(_random.NextSingle() * _range, _random.NextSingle() * _range, _random.NextSingle() * _range);
            }
        }
    }
}