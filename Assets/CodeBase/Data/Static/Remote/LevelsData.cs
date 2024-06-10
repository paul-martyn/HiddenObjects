using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Data.Static.Remote
{
    [Serializable]
    public class LevelsData
    {
        public List<LevelConfig> LevelConfigs;

        public void Log()
        {
            Debug.Log("---------------------------");
            Debug.Log("LEVELS DATA:");
            foreach (LevelConfig levelConfig in LevelConfigs)
            {
                levelConfig.Log();
            }
            Debug.Log("---------------------------");
        }
        
        public LevelConfig GetById(int levelId) => 
            LevelConfigs.FirstOrDefault(levelConfig => levelConfig.ID == levelId);
    }
}