using System.Numerics;

namespace GameServerCore
{
    public class Entity : IUpdate
    {
        private ILogger _logger;

        public Vector3 Position { get; set; }

        private IPosition? targetPos;

        public Entity(ILogger logger)
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
            _logger.Log($"EntityPos: {Position}");
        }
    }
}