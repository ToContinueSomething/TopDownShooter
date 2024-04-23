namespace Sources.Logic.Spawner
{
    public class BonusSpawner : Spawner<Bonus.Bonus>
    {
        protected override Bonus.Bonus GetEntity()
        {
            return GetRandomEntityOrNull();
        }
    }
}