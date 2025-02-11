using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Game
{
    public class BetView : MonoBehaviour
    {
        public event Action<int> OnPressChangeBetBtn;
        
        [SerializeField] 
        private Button _plusBtn;
        [SerializeField]
        private Button _minusBtn;
        [SerializeField] 
        private List<Sprite> _plusBtnSprites;
        [SerializeField] 
        private List<Sprite> _minusBtnSprites;
        [SerializeField] 
        private Text _betCountText;

        private void OnEnable()
        {
            _plusBtn.onClick.AddListener(OnPressPlusBtn);
            _minusBtn.onClick.AddListener(OnPressMinusBtn);
        }

        private void OnDisable()
        {
            _plusBtn.onClick.RemoveAllListeners();
            _minusBtn.onClick.RemoveAllListeners();
        }

        public void SetText(int value)
        {
            _betCountText.text = $"{value}.00";
        }

        public void SetStateMinusBtn(bool value)
        {
            if (_minusBtn.interactable == value)
            {
                return;
            }

            int spriteIndex = value ? 0 : 1;

            _minusBtn.image.sprite = _minusBtnSprites[spriteIndex];
            _minusBtn.interactable = value;
        }

        public void SetStatePlusBtn(bool value)
        {
            if (_plusBtn.interactable == value)
            {
                return;
            }

            int spriteIndex = value ? 0 : 1;

            _plusBtn.image.sprite = _plusBtnSprites[spriteIndex];
            _plusBtn.interactable = value;
        }

        private void OnPressMinusBtn()
        {
            Notification(-1);
        }
        
        private void OnPressPlusBtn()
        {
            Notification(1);
        }

        private void Notification(int value)
        {
            OnPressChangeBetBtn?.Invoke(value);
        }
    }
}