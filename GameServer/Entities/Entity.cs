using System.Numerics;
using Microsoft.Extensions.Logging;

namespace GameServerCore
{
    public class Entity : GameObject
    {
        private IPosition? targetPos;

        public Entity(ILogger<Entity> logger)
            : base(logger)
        {
        }

        public void SetTargetPosition(IPosition? pos)
        {
            targetPos = pos;
        }

        internal protected override void Update(TimeSpan time)
        {
            base.Update(time);

            if (targetPos != null && Position != targetPos.Pos)
            {
                Vector3 pos = Position;
                Vector3 targetPosition = targetPos.Pos;
                float speed = 0.001f;

                Vector3 direction = targetPosition - pos;

                float dist = (float)time.TotalMilliseconds * speed;
                float disToPoint = direction.Length();

                if (dist > disToPoint)
                    dist = disToPoint;

                if (disToPoint != 0)
                    Position += direction / disToPoint * dist;
                else
                    Position = targetPosition;
            }
            _logger.LogDebug($"Entity {Id} Pos: {Position}");
        }
    }
}