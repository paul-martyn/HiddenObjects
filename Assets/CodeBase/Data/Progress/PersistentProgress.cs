using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data.Static.Remote;

namespace CodeBase.Data.Progress
{
    [Serializable]
    public class PersistentProgress
    {
        public List<LevelProgress> LevelProgresses;

        public PersistentProgress(List<LevelConfig> levelConfigs)
        {
            LevelProgresses = new List<LevelProgress>(levelConfigs.Count);
          
            for (int i = 0; i < levelConfigs.Count; i++)
            {
                LevelProgress item = new()
                {
                    LevelID = levelConfigs[i].ID
                };
                LevelProgresses.Add(item);
            }
        }
        
        public LevelProgress GetById(int levelId) => 
            LevelProgresses.FirstOrDefault(progress => progress.LevelID == levelId);
    }
}