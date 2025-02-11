using System;
using UnityEngine;

using Views.Game;
using Models.Game;

namespace Controllers.Game
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] 
        private GameBallView _ballView;
        
        private Rigidbody2D _rb;
        private BallModel _model;
        
        public event Action<GameObject, bool> OnTriggerDeadZoneAction;
        public event Action<GameObject,int> OnTriggerPoinZoneAction; 
        
        private void Awake()
        {
            _model = new BallModel();
            _rb = GetComponent<Rigidbody2D>();
        }

        public void SetSkin(Sprite sprite)
        {
            _ballView.SetSprite(sprite);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            CheckTag(other.gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            CheckTag(other.gameObject);
        }

        private void MoveToNextSide()
        {
            Vector2 forceVector = Vector2.zero;

            forceVector.x = _rb.mass * _model.Direction * 10;
            
            _rb.AddForce(forceVector, ForceMode2D.Impulse);
        }

        private void CheckTag(GameObject go)
        {
            switch (go.tag)
            {
                case "StopPoint":
                    OnTriggerStopPoint();
                    break;
                case "StaticBall":
                    OnTriggerStaticBall(go);
                    break;
                case "PointZone":
                    OnTriggerPointZone();
                    break;
                case "DeadZone":
                   OnTriggerDeadZone();
                    break;
            }
        }

        private void OnTriggerStopPoint()
        {
            _rb.velocity = Vector2.zero;

            MoveToNextSide();
        }

        private void OnTriggerStaticBall(GameObject go)
        {
            if (!_model.IsTriggerFirstTime(go))
            {
                return;
            }

            StaticBallView ballView = go.GetComponent<StaticBallView>();
            
            ballView.SetText(_model.Point);
            ballView.StartAnim();
        }

        private void OnTriggerPointZone()
        {
            _model.SetIsPointBall();
            
            OnTriggerPoinZoneAction?.Invoke(gameObject,_model.PointsCount);
        }

        private void OnTriggerDeadZone()
        {
            OnTriggerDeadZoneAction?.Invoke(gameObject, _model.IsPointBall);
        }
    }
}