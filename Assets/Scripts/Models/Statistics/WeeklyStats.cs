using System.Collections.Generic;

namespace Models.Statistics
{
    public class WeeklyStats
    {
        public string weekStart = "";
        public Dictionary<string, DailyStats> dailyPoints = new();
    }
}