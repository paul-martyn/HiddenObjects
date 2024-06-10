using CodeBase.Infrastructure.States;
using CodeBase.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class MainMenuWindow :  WindowBase
    {
        [SerializeField] private RectTransform _contentParent;
        [Space(10f)]
        [SerializeField] private Button _clearSaves;
        
        public RectTransform ContentParent => _contentParent;

        private void Awake()
        {
            ClearParent();
        }

        protected override void Initialize() => 
            _clearSaves.onClick.AddListener(ClearProgress);

        private void OnDestroy() => 
            _clearSaves.onClick.RemoveListener(ClearProgress);

        private void ClearParent()
        {
            while (_contentParent.childCount > 0) 
                DestroyImmediate(_contentParent.GetChild(0).gameObject);
        }
        private void ClearProgress()
        {
            PlayerPrefs.DeleteAll();
            AllServices.Container.Single<IGameStateMachine>().Enter<BootstrapState>();
        }
    }
}