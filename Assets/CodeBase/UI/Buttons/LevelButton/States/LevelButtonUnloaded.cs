using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Buttons.LevelButton.States
{
    public class LevelButtonUnloaded : LevelButtonState
    {
        private readonly Image _previewImage;
        private readonly GameObject _failPattern;

        public LevelButtonUnloaded(ButtonAddon buttonAddon, Image previewImage, GameObject failPattern) : base(buttonAddon)
        {
            _previewImage = previewImage;
            _failPattern = failPattern;
        }

        protected override void Init() => 
            Reset();

        public override void Enter()
        {
            _failPattern.SetActive(true);
            _previewImage.color = new Color(1f, 0.73f, 0.67f);
            ButtonAddon.SetActive(false);
        }

        public override void Exit() => 
            Reset();

        private void Reset()
        {
            _failPattern.SetActive(false);
            _previewImage.color = Color.white;
        }
    }
}