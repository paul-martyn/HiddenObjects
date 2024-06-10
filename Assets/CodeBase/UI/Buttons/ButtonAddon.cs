using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Buttons
{
    [RequireComponent(typeof(Button), typeof(CanvasGroup), typeof(ButtonCustomAnimation))]
    public abstract class ButtonAddon : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private CanvasGroup _canvasGroup;
        [Space(10f)]
        [SerializeField] private ButtonCustomAnimation _buttonCustomAnimation;

        public void SetActive(bool isActive)
        {
            if (isActive)
                Active();
            else
                Inactive();
        }

        private void OnEnable() => 
            _button.onClick.AddListener(OnClick);

        private void OnDisable() => 
            _button.onClick.AddListener(OnClick);

        protected virtual void OnClick() => 
            _buttonCustomAnimation.Play();

        private void Active()
        {
            _button.interactable = true;
            _canvasGroup.interactable = true;
            _canvasGroup.alpha = 1f;
        }
            
        private void Inactive()
        {
            _button.interactable = false;
            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0.9f;
        }
    }
}