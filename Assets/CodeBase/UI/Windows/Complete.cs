using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class Complete : WindowBase
    {
        [SerializeField]
        private Button _continue;

        protected override void SubscribeUpdates()
        {
            _continue.onClick.AddListener(Continue);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _continue.onClick.RemoveListener(Continue);
        }

        private void Continue()
        {
            Debug.LogWarning("Load next level");
        }
    }
}