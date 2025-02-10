using UnityEngine;


namespace Controllers.Game
{
    public class PyramidGeneratorController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _ballPrefab;
        [SerializeField]
        private int _rows;
        [SerializeField] 
        private float _ballSize;
        [SerializeField] 
        private float _spacingX;
        [SerializeField] 
        private float _spacingY;

        private void Start()
        {
            GeneratePyramid();
        }

        private void GeneratePyramid()
        {
            for (int row = 0; row < _rows; row++)
            {
                int ballsInRow = 3 + row;
                float rowWidth = ballsInRow * (_ballSize + _spacingX) - _spacingX;

                for (int ballIndex = 0; ballIndex < ballsInRow; ballIndex++)
                {
                    float xPos = -rowWidth / 2 + ballIndex * (_ballSize + _spacingX) + _ballSize / 2;
                    float yPos = (-row * (_ballSize + _spacingY)) + 157;
                    
                    GameObject ball = Instantiate(_ballPrefab, transform);
                    RectTransform rect = ball.GetComponent<RectTransform>();
                    rect.anchoredPosition = new Vector2(xPos, yPos);
                    rect.sizeDelta = new Vector2(_ballSize, _ballSize);
                }
            }
        }
    }
}