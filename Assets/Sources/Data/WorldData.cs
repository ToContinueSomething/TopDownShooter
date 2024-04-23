namespace Sources.Data
{
    public class WorldData
    {
        public Score Score { get; private set; }

        public WorldData()
        {
            Score = new Score();
        }
    }
}