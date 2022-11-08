namespace GameServerCore
{
    public class FakeUpdate : IUpdate
    {
        private int _dealy;

        public FakeUpdate(int dealy = 1000)
        {
            _dealy = dealy;
        }

        public void Update(TimeSpan time)
        {
            Task.Delay(_dealy)
                .Wait();
        }
    }
}