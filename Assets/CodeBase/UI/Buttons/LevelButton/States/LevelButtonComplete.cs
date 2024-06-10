using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Buttons.LevelButton.States
{
    public class LevelButtonComplete : LevelButtonState
    {
        private readonly Image _previewImage;

        public LevelButtonComplete(ButtonAddon buttonAddon, Image previewImage) : base(buttonAddon)
        {
            _previewImage = previewImage;
        }

        protected override void Init() => 
            Reset();

        public override void Enter()
        {
            _previewImage.color = new Color(0.71f, 0.95f, 0.99f);
            ButtonAddon.SetActive(false);
        }

        public override void Exit() => 
            Reset();

        private void Reset()
        {
            _previewImage.color = Color.white;
        }
    }
}