using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Views.General;

namespace Views.Game
{
    public class ResultPanel : PanelView
    {
        [SerializeField] 
        private Image _bodyImage;
        [SerializeField] 
        private List<Sprite> _bodySprites;
        [SerializeField] 
        private TextMeshProUGUI _rewardText;
        [SerializeField] 
        private Text _scoreText;

        public void SetDescription(bool value)
        {
            int index = value ? 0 : 1;

            _bodyImage.sprite = _bodySprites[index];
        }

        public void SetScore(int value)
        {
            _scoreText.text = $"SCORE: {value}.00";
        }

        public void SetReward(int value)
        {
            string rewardText = value > 0 ? $"+{value}" : value.ToString();

            _rewardText.text = rewardText;
        }
    }
}