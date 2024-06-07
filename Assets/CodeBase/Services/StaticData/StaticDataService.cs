using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data.Static;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string WindowConfigsPath = "StaticData/ScreensStaticData";

        private Dictionary<WindowID, WindowConfig> _windowConfigs;
        
        public void Load()
        {
            Debug.Log($"Load static data...");
            _windowConfigs = Resources
                .Load<WindowsStaticData>(path: WindowConfigsPath)
                .ScreensData
                .ToDictionary(x => x.WindowID, x => x);
        }
        
        public WindowConfig ForWindow(WindowID windowID)
        {
            foreach (KeyValuePair<WindowID, WindowConfig> pair in _windowConfigs.Where(pair => pair.Key == windowID))
                return pair.Value;
            throw new ArgumentNullException();
        }
    }
}