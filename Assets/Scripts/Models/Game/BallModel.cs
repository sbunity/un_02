using System.Collections.Generic;
using UnityEngine;

namespace Models.Game
{
    public class BallModel
    {
        private int _pointsCount;
        private List<int> _allPoints;
        private List<GameObject> _triggerObjects;
        
        public int Point => GetRandomPoint();
        public int Direction => GetDirection();

        public BallModel()
        {
            _triggerObjects = new List<GameObject>();
            
            _pointsCount = 0;
            
            FillPoints();
        }

        public bool IsTriggerFirstTime(GameObject go)
        {
            if (_triggerObjects.Contains(go))
            {
                return false;
            }
            else
            {
                _triggerObjects.Add(go);
                return true;
            }
        }

        private int GetDirection()
        {
            int random = Random.Range(0, 10);

            int direction = random < 5 ? -1 : 1;

            return direction;
        }

        private int GetRandomPoint()
        {
            int random = Random.Range(0, _allPoints.Count);

            _pointsCount += _allPoints[random];

            return _allPoints[random];
        }

        private void FillPoints()
        {
            _allPoints = new List<int>();
            
            int startPoint = -25;
            int endPoint = 25;

            for (int i = startPoint; i < endPoint; i+=5)
            {
                int point = i;
                
                _allPoints.Add(point);
            }
        }
    }
}