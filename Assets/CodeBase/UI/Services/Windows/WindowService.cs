using System;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Windows;

namespace CodeBase.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        MainMenuWindow IWindowService.MainMenuWindow => _mainMenuWindow;
        private MainMenuWindow _mainMenuWindow;

        LoadingWindow IWindowService.LoadingWindow => _loadingWindow;
        private LoadingWindow _loadingWindow;
        
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowID windowID)
        {
            switch (windowID)
            {
                case WindowID.MainMenu:
                    _mainMenuWindow = _uiFactory.CreateMainMenuWindow();
                    break;
                case WindowID.Loading:
                    _loadingWindow = _uiFactory.CreateLoadingWindow();
                    break;
                case WindowID.Unknown:
                default: throw new ArgumentOutOfRangeException(nameof(windowID), windowID, null);
            }
        }
    }
}