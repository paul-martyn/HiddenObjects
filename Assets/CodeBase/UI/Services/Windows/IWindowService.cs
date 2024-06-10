using CodeBase.Services;
using CodeBase.UI.Windows;

namespace CodeBase.UI.Services.Windows
{
    public interface IWindowService : IService
    {
        public MainMenuWindow MainMenuWindow { get; }
        public LoadingWindow LoadingWindow { get; }

        public void Open(WindowID windowID);

    }
}