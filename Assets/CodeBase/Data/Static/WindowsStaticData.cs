using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Data.Static
{
    [CreateAssetMenu(fileName = "WindowStaticData", menuName = "StaticData/Windows static data")]
    public class WindowsStaticData : ScriptableObject
    {
        public List<WindowConfig> ScreensData;
    }
}