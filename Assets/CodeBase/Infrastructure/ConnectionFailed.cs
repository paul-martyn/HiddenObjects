using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure
{
    public class ConnectionFailed : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _tryAgain;
        
        private void Awake() => 
            DontDestroyOnLoad(this);

        private void Start()
        {
            //_tryAgain += 
        }

        public void Show()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Hide() => 
            Reset();

        private void Reset()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}