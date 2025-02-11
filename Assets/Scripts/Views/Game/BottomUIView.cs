using System;
using UnityEngine;
using UnityEngine.UI;

using Views.General;
using Enums;

namespace Views.Game
{
    public class BottomUIView : MonoBehaviour
    {
        [SerializeField] 
        private BetView _betView;
        [SerializeField]
        private BalanceUpdater _winBalanceUpdater;
        [SerializeField]
        private Button _startBtn;
        [SerializeField] 
        private Text _betCountText;

        public event Action OnPressStartBtnAction;
        public event Action<int> OnPressBetBtnAction;

        private void OnEnable()
        {
            _startBtn.onClick.AddListener(OnPressStartBtn);

            _betView.OnPressChangeBetBtn += OnPressBetBtn;
        }

        private void OnDisable()
        {
            _startBtn.onClick.RemoveAllListeners();
            
            _betView.OnPressChangeBetBtn -= OnPressBetBtn;
        }

        public void SetState(GameState state)
        {
            switch (state)
            {
                case GameState.Start:
                    SetStartState();
                    break;
                case GameState.Game:
                    SetGameState();
                    break;
            }
        }

        public void SetBet(int value)
        {
            _betView.SetText(value);
            _betCountText.text = $"{value}.00";
        }

        public void UpdateWinBalance(int value)
        {
            _winBalanceUpdater.UpdateText(value);
        }

        public void SetStateBetBtns(bool minusBtnState, bool plusBtnState)
        {
            _betView.SetStateMinusBtn(minusBtnState);
            _betView.SetStatePlusBtn(plusBtnState);
        }

        private void SetStartState()
        {
            SetStateBetView(true);
            SetStateStartBtn(true);
            SetStateBetCountText(false);
            SetStateWinBalance(false);
        }

        private void SetGameState()
        {
            SetStateBetView(false);
            SetStateStartBtn(false);
            SetStateBetCountText(true);
            SetStateWinBalance(true);
        }

        private void SetStateBetView(bool value)
        {
            _betView.gameObject.SetActive(value);
        }
        
        private void SetStateStartBtn(bool value)
        {
            _startBtn.gameObject.SetActive(value);
        }

        private void SetStateBetCountText(bool value)
        {
            _betCountText.gameObject.SetActive(value);
        }

        private void SetStateWinBalance(bool value)
        {
            _winBalanceUpdater.gameObject.SetActive(value);
        }

        private void OnPressStartBtn()
        {
            Notification();
        }

        private void OnPressBetBtn(int value)
        {
            OnPressBetBtnAction?.Invoke(value);
        }

        private void Notification()
        {
            OnPressStartBtnAction?.Invoke();
        }
    }
}