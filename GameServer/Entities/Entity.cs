using System.Numerics;
using Microsoft.Extensions.Logging;

namespace GameServerCore
{
    public class Entity : GameObject
    {
        private ILogger _logger;

        private IPosition? targetPos;

        public Entity(ILogger<Entity> logger)
        {
            _logger = logger;
        }

        public void GoTo(IPosition pos)
        {
            targetPos = pos;
        }

        public void Update(TimeSpan time)
        {
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