using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Views.General;

namespace Views.Game
{
    public class ConfirmationPanel : PanelView
    {
        [SerializeField] 
        private Image _bodyImage;
        [SerializeField] 
        private List<Sprite> _bodySprites;

        public void SetBodySprite(bool value)
        {
            int index = value ? 0 : 1;

            _bodyImage.sprite = _bodySprites[index];
        }
    }
}