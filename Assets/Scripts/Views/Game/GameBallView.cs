using UnityEngine;
using UnityEngine.UI;

namespace Views.Game
{
    public class GameBallView : MonoBehaviour
    {
        [SerializeField] 
        private Image _image;

        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}