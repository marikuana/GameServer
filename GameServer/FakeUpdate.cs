namespace GameServerCore
{
    public class FakeUpdate : IUpdate
    {
        public void Update(TimeSpan time)
        {
            Task.Delay(1000)
                .Wait();
        }
    }
}