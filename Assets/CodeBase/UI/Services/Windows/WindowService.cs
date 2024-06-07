using CodeBase.UI.Services.Factory;

namespace CodeBase.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;
        
        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;;
        }
        
        public void Open(WindowID windowID)
        {
            switch (windowID)
            {
                case WindowID.MainMenu:
                    _uiFactory.CreateMainMenuWindow();
                    break;
                case WindowID.Complete:
                    _uiFactory.CreateCompleteWindow();
                    break;
                case WindowID.Trouble:
                    _uiFactory.CreatTroubleWindow();
                    break;
            }
        }
    }
}