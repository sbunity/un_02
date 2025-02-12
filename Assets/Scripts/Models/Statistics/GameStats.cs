namespace Models.Statistics
{
    [System.Serializable]
    public class GameStats
    {
        public int _pointsCount;
        public int _coefficient;
        public int _ballsCount;
        public bool _isWin;

        public GameStats(int points, int coefficient, int balls, bool isWin)
        {
            _pointsCount = points;
            _coefficient = coefficient;
            _ballsCount = balls;
            _isWin = isWin;
        }
    }
}