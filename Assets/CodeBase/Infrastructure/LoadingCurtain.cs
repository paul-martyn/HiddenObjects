using DG.Tweening;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _duration;

        private Tween _showTween;
        private Tween _hideTween;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
            CacheAnimation();
        }

        private void CacheAnimation()
        {
            _hideTween =  _canvasGroup.DOFade(0f, _duration)
                .OnComplete(Reset)
                .SetAutoKill(false);
            _hideTween.Rewind();
            
            _showTween =  _canvasGroup.DOFade(1f, _duration)
                .SetAutoKill(false);
            _showTween.Rewind();
        }

        public void Show()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
        }
        
        public void ShowAnim()
        {
            _canvasGroup.blocksRaycasts = true;
            _showTween.Restart();
        }

        public void Hide() => 
            _hideTween.Restart();

        private void Reset()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}