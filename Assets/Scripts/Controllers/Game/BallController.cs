using UnityEngine;
using Views.Game;
using Models.Game;

namespace Controllers.Game
{
    public class BallController : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private BallModel _model;
        
        private void Awake()
        {
            _model = new BallModel();
            _rb = GetComponent<Rigidbody2D>();
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

            forceVector.x = _rb.mass * _model.Direction * 100;
            
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
    }
}