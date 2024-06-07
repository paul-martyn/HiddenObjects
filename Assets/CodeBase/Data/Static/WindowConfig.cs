using System;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;

namespace CodeBase.Data.Static
{
    [Serializable]
    public class WindowConfig
    {
        public WindowID WindowID;
        public WindowBase Prefab;
    }
}