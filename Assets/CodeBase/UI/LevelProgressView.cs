using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class LevelProgressView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _fill;
        
        public void RefreshProgress(int current, int target)
        {
            _text.SetText($"{current} / {target}");
            _fill.fillAmount = (float) current / target ;
        }
    }
}