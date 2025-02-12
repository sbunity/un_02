using System;
using System.Collections.Generic;
using System.Globalization;

using UnityEngine;
using UnityEngine.UI;
using Models.Statistics;

namespace Views.Statistics
{
    public class DailyStaticsView : MonoBehaviour
    {
        [SerializeField] 
        private Text _dateText;
        [SerializeField] 
        private GameObject _gameStatisticPrefab;
        [SerializeField] 
        private RectTransform _contentRect;

        private List<GameObject> _gameStates;

        public void SetDateText(DateTime date)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            
            _dateText.text = date.ToString("dd MMMM, yyyy", cultureInfo);
        }

        public void SetGameStatistics(List<GameStats> gameStats)
        {
            if (_gameStates != null)
            {
                ClearGameStates();
            }
            else
            {
                _gameStates = new List<GameObject>();
            }

            foreach (var gameState in gameStats)
            {
                GameObject go = Instantiate(_gameStatisticPrefab, _contentRect);
                
                _gameStates.Add(go);

                GameStatisticView gameStatisticView = go.GetComponent<GameStatisticView>();
                
                gameStatisticView.SetReward(gameState._pointsCount);
                gameStatisticView.SetCoefficient(gameState._coefficient);
                gameStatisticView.SetBallCount(gameState._ballsCount);
            }
        }

        private void ClearGameStates()
        {
            for (int i = 0; i < _gameStates.Count; i++)
            {
                Destroy(_gameStates[i].gameObject);
            }
            
            _gameStates.Clear();
        }
    }
}