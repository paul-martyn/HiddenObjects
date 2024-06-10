using UnityEngine;

namespace CodeBase.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        private void Start() => 
            Initialize();

        protected virtual void Initialize(){}
        
        public void Show()
        {
            GetComponent<Canvas>().enabled = true;
        }
        
        public void Hide()
        {
            GetComponent<Canvas>().enabled = false;
        }

        public void Close() => 
            Destroy(gameObject);
    }
}