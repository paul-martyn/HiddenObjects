using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.LevelButton
{
    public class LevelButtonInactive : LevelButtonState
    {
        private readonly CanvasGroup _canvasGroup;
        private readonly Button _button;
        private readonly Image _image;

        public LevelButtonInactive(CanvasGroup canvasGroup, Button button, Image image)
        {
            _canvasGroup = canvasGroup;
            _button = button;
            _image = image;
        }

        public override void Enter()
        {
            _image.color = new Color(1f, 0.41f, 0.41f);
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