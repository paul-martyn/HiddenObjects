using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.LevelButton
{
    public abstract class LevelButtonState
    {
        private readonly Image _image;
        private readonly TMP_Text _title;
        private readonly CanvasGroup _loadingFailedPatter;
        private readonly LoadingProcessAnimation _loadingProcessAnimation;

        public abstract void Enter();
        public abstract void Exit();

        public virtual void OnClick()
        {
        }
    }
}