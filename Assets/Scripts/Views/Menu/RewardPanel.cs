using TMPro;
using UnityEngine;
using Views.General;

namespace Views.Menu
{
    public class RewardPanel : PanelView
    {
        [SerializeField] 
        private TextMeshProUGUI _rewardText;

        public void UpdateReward(int value)
        {
            _rewardText.text = $"+{value}";
        }
    }
}