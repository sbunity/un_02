using System.Collections.Generic;
using UnityEngine;

namespace Models.Game
{
    public class SpawnModel
    {
        private int _spritesCount;
        private int _currentSpriteIndex;
        private int _ballMissedCount;
        private List<GameObject> _activeBalls;

        private const int MAX_MISSED_BALLS = 3;

        public int CurrentSpriteIndex => _currentSpriteIndex;
        public int MissedBallsCount => _ballMissedCount;
        public bool CanSpawnBall => _ballMissedCount < MAX_MISSED_BALLS;
        public List<GameObject> ActiveBalls => _activeBalls;

        public SpawnModel(int spritesCount)
        {
            _spritesCount = spritesCount;
            _currentSpriteIndex = 0;
            _ballMissedCount = 0;

            _activeBalls = new List<GameObject>();
        }

        public void AddBall(GameObject ball)
        {
            _activeBalls.Add(ball);
        }

        public void AddMissedBallCount()
        {
            _ballMissedCount++;
        }

        public void UpdateSpriteIndex()
        {
            _currentSpriteIndex++;

            if (_currentSpriteIndex == _spritesCount)
            {
                _currentSpriteIndex = 0;
            }
        }
    }
}