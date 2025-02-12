using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Statistics
{
    public class WeeklyCountView : MonoBehaviour
    {
        [SerializeField] 
        private List<Text> _countGamesText;
        [SerializeField]
        private Image _backImage;

        public void SetTexts(List<int> counts)
        {
            for (int i = 0; i < _countGamesText.Count; i++)
            {
                _countGamesText[i].text = counts[i].ToString();
            }
        }

        public void SetState(bool value)
        {
            _backImage.enabled = value;
        }
    }
}