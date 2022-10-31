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
                float speed = 0.001f;

                float dis = (float)time.TotalMilliseconds * speed;
                float disToPoint = (Position - targetPos.Pos).Length();

                if (dis > disToPoint)
                    dis = disToPoint;

                Vector3 vector = Vector3.Normalize(targetPos.Pos - Position);
                Position += vector * dis; 
            }
            _logger.LogDebug($"Entity {Id} Pos: {Position}");
        }
    }
}