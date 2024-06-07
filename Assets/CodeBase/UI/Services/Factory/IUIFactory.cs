using CodeBase.Services;
using CodeBase.UI.Windows;

namespace CodeBase.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        public void CreateUIRoot();
        public void CreatTroubleWindow();
        public void CreateMainMenuWindow();
        public Level CreatLevelWindow();
        public void CreateCompleteWindow();
    }
}