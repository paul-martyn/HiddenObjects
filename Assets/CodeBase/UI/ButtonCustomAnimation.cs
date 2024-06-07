using DG.Tweening;
using UnityEngine;

namespace CodeBase.UI
{
    [RequireComponent(typeof(CanvasGroup), typeof(RectTransform))]
    public class ButtonCustomAnimation : MonoBehaviour
    {
        [SerializeField] private float _duration = 0.3f;
        [SerializeField] [Range(-1f, 1f)] private float _scale = 0.1f;

        private Sequence _sequence;
        private Tween _scaleAnimation;
        private Tween _fadeAnimation;
        
        private void Awake() => 
            CacheAnimation();

        public void Play() => 
            _sequence.Restart();

        private void CacheAnimation()
        {
            _sequence = DOTween.Sequence();
            _sequence.Join(GetComponent<RectTransform>().DOPunchScale(Vector3.one * _scale, _duration, 1, 0f));
            _sequence.Join(GetComponent<CanvasGroup>().DOFade(0.9f, _duration / 2).SetLoops(2, LoopType.Yoyo));
            _sequence.Pause();
            _sequence.SetAutoKill(false);
            _sequence.SetLink(gameObject);
        }
    }
}