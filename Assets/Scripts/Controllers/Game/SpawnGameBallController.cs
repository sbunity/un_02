using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Models.Game;
using Enums;

namespace Controllers.Game
{
    public class SpawnGameBallController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _ballPrefab;
        [SerializeField]
        private List<GameObject> _basketElements;
        [SerializeField] 
        private List<Sprite> _ballSprites;

        private SpawnModel _model;
        private Coroutine _spawnCoroutine;

        public event Action<int> OnAddPointsAction;
        public event Action OnMissedMaxBallsAction;
        public event Action<int> OnMissedBallAction;

        public void Initialize()
        {
            _model = new SpawnModel(_ballSprites.Count);
        }

        public void SetState(GameState state)
        {
            switch (state)
            {
                case GameState.Game:
                    StartSpawnBalls();
                    break;
                case GameState.Finish:
                    StopSpawnBalls();
                    break;
            }
        }

        private void StartSpawnBalls()
        {
            _basketElements.ForEach(x => x.SetActive(true));
            
            _spawnCoroutine = StartCoroutine(SpawnBalls());
        }

        private void StopSpawnBalls()
        {
            StopCoroutine(_spawnCoroutine);

            List<GameObject> balls = new List<GameObject>(_model.ActiveBalls);

            foreach (var ball in balls.Where(ball => ball != null))
            {
                Destroy(ball);
            }
        }

        private void OnBallTriggerDeadZone(GameObject ball, bool value)
        {
            BallController ballController = ball.GetComponent<BallController>();

            ballController.OnTriggerDeadZoneAction -= OnBallTriggerDeadZone;
            
            Destroy(ball);

            if (value)
            {
                return;
            }
            
            _model.AddMissedBallCount();
            
            OnMissedBallAction?.Invoke(_model.MissedBallsCount);
        }

        private void OnBallTriggerPointZone(GameObject ball,int points)
        {
            BallController ballController = ball.GetComponent<BallController>();

            ballController.OnTriggerPoinZoneAction -= OnBallTriggerPointZone;
            
            OnAddPointsAction?.Invoke(points);
        }

        private IEnumerator SpawnBalls()
        {
            while (_model.CanSpawnBall)
            {
                GameObject ball = Instantiate(_ballPrefab, transform);
                
                _model.AddBall(ball);
                
                BallController ballController = ball.GetComponent<BallController>();
                
                ballController.SetSkin(_ballSprites[_model.CurrentSpriteIndex]);

                ballController.OnTriggerDeadZoneAction += OnBallTriggerDeadZone;
                ballController.OnTriggerPoinZoneAction += OnBallTriggerPointZone;
                
                _model.UpdateSpriteIndex();

                yield return new WaitForSeconds(0.6f);
            }
            
            OnMissedMaxBallsAction?.Invoke();
        }
    }
}