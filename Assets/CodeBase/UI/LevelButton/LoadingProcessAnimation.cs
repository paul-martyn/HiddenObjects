using UnityEngine;

namespace CodeBase.UI.LevelButton
{
    public class LoadingProcessAnimation : MonoBehaviour
    {
        [SerializeField] private Animation _animation;
        [SerializeField] private CanvasGroup _canvasGroup;

        public void Play()
        {
            _canvasGroup.alpha = 1f;
            _animation.Play();
        }
        
        public void Stop()
        {
            _canvasGroup.alpha = 0f;
            _animation.Stop();
        }
    }
}