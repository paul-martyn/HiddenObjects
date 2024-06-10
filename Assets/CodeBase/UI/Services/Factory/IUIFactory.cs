using CodeBase.Services;
using CodeBase.UI.Windows;

namespace CodeBase.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        public void CreateUIRoot();
        public MainMenuWindow CreateMainMenuWindow();
        public LoadingWindow CreateLoadingWindow();
    }
}