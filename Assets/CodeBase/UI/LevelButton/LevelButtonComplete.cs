using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.LevelButton
{
    public class LevelButtonComplete : LevelButtonState
    {
        private readonly CanvasGroup _canvasGroup;
        private readonly Button _button;
        private readonly Image _image;

        public LevelButtonComplete(CanvasGroup canvasGroup, Button button, Image image)
        {
            _canvasGroup = canvasGroup;
            _button = button;
            _image = image;
        }

        public override void Enter()
        {
            _image.color = new Color(0.55f, 1f, 0.76f);
            _button.interactable = false;
            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0.8f;
        }

        public override void Exit()
        {
            _image.color = Color.white;
        }
    }
}