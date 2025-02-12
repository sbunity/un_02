using System;
using System.Collections.Generic;

using UnityEngine;
using Models.Statistics;

namespace Models.Scenes
{
    public class StatisticsSceneModel
    {
        private int _currentDayIndex;
        
        private const int InitialMaxPoints = 100;
        private const int MarksCount = 7;

        public int CurrentDayIndex => _currentDayIndex;

        public StatisticsSceneModel()
        {
            FillDayIndex();
        }

        public void ChangeSelectedGraphIndex(int index)
        {
            _currentDayIndex = index;
        }

        public DateTime GetDate(int index)
        {
            List<string> days = StatisticsModel.GetAllSavedDays();

            DateTime date = DateTime.Parse(days[index]);

            return date;
        }

        public List<GameStats> GetDailyGameStates()
        {
            List<string> days = StatisticsModel.GetAllSavedDays();

            return StatisticsModel.GetDailyGames(days[_currentDayIndex]);
        }

        public List<int> GetYMarks()
        {
            List<int> yMarks = new List<int>();
            int maxPoints = StatisticsModel.GetWeeklyMaxPoints();
            
            int upperBound = GetUpperBound(maxPoints);
            
            if (maxPoints <= 0)
            {
                for (int i = 0; i <= 600; i += 100)
                {
                    yMarks.Add(i);
                }
            }
            else
            {
                int step = Mathf.Max(upperBound / 6, 100);

                for (int i = 0; i <= upperBound; i += step)
                {
                    yMarks.Add(i);
                }
            }

            return yMarks;
        }

        public List<float> GetLossFillAmounts()
        {
            List<float> lossFillAmounts = new List<float>();
            List<string> days = StatisticsModel.GetAllSavedDays();

            int maxPoints = GetUpperBound(StatisticsModel.GetWeeklyMaxPoints());

            foreach (var day in days)
            {
                int lossPoints = StatisticsModel.GetDailyLossPoints(day);
                
                float fillAmount;
                
                if (maxPoints > 0)
                {
                    fillAmount = (float)lossPoints / (float)maxPoints;
                }
                else
                {
                    fillAmount = lossPoints > 0 ? 0.01f : 0f;
                }

                if (fillAmount < 0.01f)
                {
                    fillAmount = 0.01f;
                }

                lossFillAmounts.Add(Mathf.Clamp01(fillAmount));
            }

            return lossFillAmounts;
        }
        
        public List<float> GetWinFillAmounts()
        {
            List<float> winFillAmounts = new List<float>();
            List<string> days = StatisticsModel.GetAllSavedDays();

            int maxPoints = GetUpperBound(StatisticsModel.GetWeeklyMaxPoints());

            foreach (var day in days)
            {
                int winPoints = StatisticsModel.GetDailyWinPoints(day);
                
                float fillAmount;
                if (maxPoints > 0)
                {
                    fillAmount = (float)winPoints / maxPoints;
                }
                else
                {
                    fillAmount = winPoints > 0 ? 0.01f : 0f;
                }
                
                if (fillAmount < 0.01f)
                {
                    fillAmount = 0.01f;
                }

                winFillAmounts.Add(Mathf.Clamp01(fillAmount));
            }

            return winFillAmounts;
        }

        private int GetUpperBound(int maxPoints)
        {
            if (maxPoints <= 0)
            {
                maxPoints = InitialMaxPoints;
            }
            
            int upperBound = ((maxPoints / 100) + 1) * 100;
            
            while (upperBound / 100 < MarksCount)
            {
                upperBound += 100;
            }

            return upperBound;
        }

        private void FillDayIndex()
        {
            List<string> days = StatisticsModel.GetAllSavedDays();

            for (int i = 0; i < days.Count; i++)
            {
                DateTime date = DateTime.Parse(days[i]);

                if (date == DateTime.Now.Date)
                {
                    _currentDayIndex = i;
                }
            }
        }
    }
}