using Sources.StaticData;
using UnityEngine;

namespace Sources.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string LevelPath = "StaticData/LevelData";
        
        private LevelStaticData _levelStaticData;

        public void Load()
        {      
            _levelStaticData = Resources.Load<LevelStaticData>(LevelPath);
        }

        public LevelStaticData GetLevelData() => _levelStaticData;
    }
}