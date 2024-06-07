using CodeBase.Data.Static;
using CodeBase.UI.Services.Windows;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        public void Load();
        public WindowConfig ForWindow(WindowID windowID);
    }
}