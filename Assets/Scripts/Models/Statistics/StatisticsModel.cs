using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Models.Statistics
{
    public static class StatisticsModel
    {
        private const string StatsKey = "StatisticsModel.GameWeeklyStats";

        public static int GetWeeklyMaxPoints()
        {
            int maxWinPoints = GetMaxWinPointsForWeek();
            int maxLossPoints = GetMaxLossPointsForWeek();

            return Math.Max(maxWinPoints,Mathf.Abs(maxLossPoints));
        }

        public static List<GameStats> GetDailyGames(string date)
        {
            WeeklyStats currentStats = LoadStats();
            
            if (currentStats.dailyPoints.ContainsKey(date))
            {
                return new List<GameStats>(currentStats.dailyPoints[date]._games);
            }

            return new List<GameStats>();
        }

        public static List<string> GetAllSavedDays()
        {
            WeeklyStats currentStats = LoadStats();
            
            return new List<string>(currentStats.dailyPoints.Keys);
        }

        public static int GetDailyWinPoints(string date)
        {
            WeeklyStats currentStats = LoadStats();
            
            if (currentStats.dailyPoints.ContainsKey(date))
            {
                int totalPoints = 0;
                foreach (var game in currentStats.dailyPoints[date]._games)
                {
                    if (game._isWin)
                    {
                        totalPoints += game._pointsCount;
                    }
                }

                return totalPoints;
            }

            return 0;
        }

        public static int GetDailyLossPoints(string date)
        {
            WeeklyStats currentStats = LoadStats();
            
            if (currentStats.dailyPoints.ContainsKey(date))
            {
                int totalPoints = 0;
                foreach (var game in currentStats.dailyPoints[date]._games)
                {
                    if (!game._isWin)
                    {
                        totalPoints += game._pointsCount;
                    }
                }

                return Mathf.Abs(totalPoints);
            }

            return 0;
        }

        private static int GetMaxWinPointsForWeek()
        {
            WeeklyStats currentStats = LoadStats();

            int maxWinPoints = 0;

            foreach (var day in currentStats.dailyPoints.Values)
            {
                int dailyWinPoints = 0;
                
                foreach (var game in day._games)
                {
                    if (game._isWin)
                    {
                        dailyWinPoints += game._pointsCount;
                    }
                }
                
                if (dailyWinPoints > maxWinPoints)
                {
                    maxWinPoints = dailyWinPoints;
                }
            }

            return maxWinPoints;
        }

        private static int GetMaxLossPointsForWeek()
        {
            WeeklyStats currentStats = LoadStats();
    
            int maxLossPoints = 0;

            foreach (var day in currentStats.dailyPoints.Values)
            {
                int dailyLossPoints = 0;
                
                foreach (var game in day._games)
                {
                    if (!game._isWin)
                    {
                        dailyLossPoints += game._pointsCount;
                    }
                }
                
                if (dailyLossPoints > maxLossPoints)
                {
                    maxLossPoints = dailyLossPoints;
                }
            }

            return maxLossPoints;
        }
        
        private static WeeklyStats  LoadStats()
        {
            WeeklyStats currentStats = new WeeklyStats();

            if (PlayerPrefs.HasKey(StatsKey))
            {
                string json = PlayerPrefs.GetString(StatsKey);
                currentStats = JsonConvert.DeserializeObject<WeeklyStats>(json);

                if (IsNewWeek(currentStats.weekStart))
                {
                    return ResetStats();
                }
                else
                {
                    return EnsureFullWeek(currentStats);
                }
            }
            else
            {
                return ResetStats();
            }
        }

        private static bool IsNewWeek(string weekStart)
        {
            if (string.IsNullOrEmpty(weekStart))
            {
                return true;
            }

            if (!DateTime.TryParse(weekStart, out DateTime lastWeekStart))
            {
                return true;
            }

            DateTime currentWeekStart = GetWeekStartDate(DateTime.Now);
    
            bool isNewWeek = lastWeekStart != currentWeekStart;
    
            return isNewWeek;
        }

        private static DateTime GetWeekStartDate(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-diff).Date;
        }

        private static WeeklyStats ResetStats()
        {
            WeeklyStats currentStats = new WeeklyStats
            {
                weekStart = GetWeekStartDate(DateTime.Now).ToString("yyyy-MM-dd"),
                dailyPoints = new Dictionary<string, DailyStats>()
            };

            currentStats = EnsureFullWeek(currentStats);
            SaveStats(currentStats);

            return currentStats;
        }

        private static WeeklyStats EnsureFullWeek(WeeklyStats currentStats)
        {
            DateTime weekStartDate = DateTime.Parse(currentStats.weekStart);

            for (int i = 0; i < 7; i++)
            {
                string day = weekStartDate.AddDays(i).ToString("yyyy-MM-dd");

                if (!currentStats.dailyPoints.ContainsKey(day))
                {
                    currentStats.dailyPoints[day] = new DailyStats();
                }
            }

            SaveStats(currentStats);
            
            return currentStats;
        }

        public static void AddGame(int points, int coefficient, int balls, bool isWin)
        {
            WeeklyStats currentStats = LoadStats();
            
            string today = DateTime.Now.ToString("yyyy-MM-dd");

            if (!currentStats.dailyPoints.ContainsKey(today))
            {
                currentStats.dailyPoints[today] = new DailyStats();
            }

            DailyStats dailyStats = currentStats.dailyPoints[today];
            dailyStats._games.Add(new GameStats(points, coefficient, balls, isWin));
            dailyStats._gamesCount++;
            
            SaveStats(currentStats);
        }

        private static void SaveStats(WeeklyStats currentStats)
        {
            string json = JsonConvert.SerializeObject(currentStats);
            PlayerPrefs.SetString(StatsKey, json);
            PlayerPrefs.Save();
        }
    }
}