using CodeBase.Services;

namespace CodeBase.UI.Services.Windows
{
    public interface IWindowService : IService
    {
        public void Open(WindowID windowID);

    }
}