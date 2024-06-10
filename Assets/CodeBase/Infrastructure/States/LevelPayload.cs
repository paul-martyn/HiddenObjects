using System;
using CodeBase.UI.Buttons.LevelButton.States;

namespace CodeBase.Infrastructure.States
{
    [Serializable]
    public class LevelPayload
    {
        public int LevelID;
        public ILevelButtonStateSwitcher Switcher;

        public LevelPayload(int levelID, ref ILevelButtonStateSwitcher switcher)
        {
            LevelID = levelID;
            Switcher = switcher;
        }
    }
}