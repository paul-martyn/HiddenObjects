using CodeBase.Infrastructure.Factory;
using CodeBase.Services;
using CodeBase.UI.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.LevelButton
{
    public class LevelButtonActive : LevelButtonState
    {
        private readonly UiElements.LevelButton _levelButton;
        private readonly CanvasGroup _canvasGroup;
        private readonly Button _button;
        private readonly Image _levelPreview;
        private readonly ButtonCustomAnimation _buttonCustomAnimation;
        private readonly MainMenu _mainMenu;

        public LevelButtonActive(CanvasGroup canvasGroup, Button button, Image levelPreview,
            ButtonCustomAnimation buttonCustomAnimation, UiElements.LevelButton levelButton)
        {
            _canvasGroup = canvasGroup;
            _button = button;
            _levelPreview = levelPreview;
            _buttonCustomAnimation = buttonCustomAnimation;
            _levelButton = levelButton;
        }

        public override void Enter()
        {
            _canvasGroup.interactable = true;
            _canvasGroup.alpha = 1f;
            _button.interactable = true;
        }

        public override void Exit()
        {
        }

        public override void OnClick()
        {
            _buttonCustomAnimation.Play();
            AllServices.Container.Single<IGameFactory>().CreateLevel(_levelButton.LevelID);
            _levelButton.MainMenu.Close();
        }
    }
}