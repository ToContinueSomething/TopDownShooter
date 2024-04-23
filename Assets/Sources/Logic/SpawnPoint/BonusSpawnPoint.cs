namespace Sources.Logic.SpawnPoint
{
    public class BonusSpawnPoint : SpawnPoint
    {
        public Bonus.Bonus Bonus { get; private set; }
        public bool IsBusy => Bonus != null;
        
        public void SetBonus(Bonus.Bonus bonus)
        {
            Bonus = bonus;
            Bonus.Collected += OnCollected;
        }
        
        private void OnCollected()
        {
            Bonus.Collected -= OnCollected;
            Bonus = null;
        }
    }
}