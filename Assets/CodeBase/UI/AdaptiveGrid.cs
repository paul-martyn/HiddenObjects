using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class AdaptiveGrid : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        [SerializeField] private RectTransform _parentRectTransform;
        [Space(10f)]
        [SerializeField] private List<GreedRatioThreshold> _thresholds;

        private void Start()
        {
            SortThresholds();
            AdaptGridLayout();
        }

        private void AdaptGridLayout()
        {
            Rect rect = _parentRectTransform.rect;
            float aspectRatio = rect.width / rect.height;
            int columns = 1;
            foreach (GreedRatioThreshold threshold in _thresholds)
            {
                if (aspectRatio >= threshold.Ratio)
                {
                    columns = threshold.ColumnCount;
                }
                else
                {
                    break;
                }
            }
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = columns;
        }

        private void SortThresholds() => 
            _thresholds.Sort((a, b) => a.Ratio.CompareTo(b.Ratio));
    }
    
    [Serializable]
    public class GreedRatioThreshold
    {
        public float Ratio;
        public int ColumnCount;
    }
}
