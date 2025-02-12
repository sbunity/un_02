using System.Collections.Generic;

namespace Models.Statistics
{
    [System.Serializable]
    public class DailyStats
    {
        public int _gamesCount;
        public List<GameStats> _games;

        public DailyStats()
        {
            _gamesCount = 0;
            _games = new List<GameStats>();
        }
    }
}