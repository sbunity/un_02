using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Game
{
    public class MissedBallsView : MonoBehaviour
    {
        [SerializeField]
        private List<Image> _ballsImage;
        [SerializeField]
        private Color _activeColor;
        [SerializeField] 
        private Color _disableColor;

        public void SetState(int value)
        {
            for (int i = 0; i < _ballsImage.Count; i++)
            {
                Color color = i < value ? _disableColor : _activeColor;

                _ballsImage[i].color = color;
            }
        }
    }
}