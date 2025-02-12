using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Statistics
{
    public class GameStatisticView : MonoBehaviour
    {
        [SerializeField] 
        private Text _rewardText;
        [SerializeField] 
        private Text _coeffText;
        [SerializeField] 
        private Text _ballsCountText;
        [SerializeField] 
        private List<Color> _rewardColors;

        public void SetReward(int value)
        {
            string valueText = value > 0 ? "+" + value : value.ToString(); 
            
            _rewardText.text = valueText + ".00";
            
            int index = value > 0 ? 0: 1;

            _rewardText.color = _rewardColors[index];
        }

        public void SetCoefficient(int value)
        {
            _coeffText.text = $"{value}.00";
        }

        public void SetBallCount(int value)
        {
            _ballsCountText.text = value.ToString();
        }
    }
}