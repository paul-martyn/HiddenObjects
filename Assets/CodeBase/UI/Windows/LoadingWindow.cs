using CodeBase.UI.Buttons.LevelButton;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public class LoadingWindow : WindowBase
    {
        [SerializeField] private LoadingProcessAnimation _loadingAnimation;

        private void OnEnable() => 
            _loadingAnimation.Play();

        private void OnDisable() => 
            _loadingAnimation.Stop();
    }
}
