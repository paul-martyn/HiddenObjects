using CodeBase.UI.UiElements;
using UnityEngine;

namespace CodeBase.UI
{
    public interface IRemoteImageHandler
    {
        public void OnImageLoadStateChanged(LoadingState state, Sprite resource);
    }
}
