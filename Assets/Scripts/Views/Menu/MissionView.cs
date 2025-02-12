using System;
using UnityEngine;
using UnityEngine.UI;
using Enums;

namespace Views.Menu
{
    public class MissionView : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _checkGameObject;
        [SerializeField] 
        private GameObject _rewardBody;
        [SerializeField] 
        private TimerView _timerView;
        [SerializeField] 
        private Button _getBtn;
        [SerializeField] 
        private MissionType _missionType;

        public event Action<MissionType> OnPressGetBtnAction;

        private void OnEnable()
        {
            _getBtn.onClick.AddListener(Notification);
        }

        private void OnDisable()
        {
            _getBtn.onClick.RemoveAllListeners();
        }

        public void SetState(MissionState state)
        {
            switch (state)
            {
                case MissionState.Default:
                    SetDefaultState();
                    break;
                case MissionState.Completed:
                    SetCompletedState();
                    break;
                case MissionState.Time:
                    SetTimeState();
                    break;
            }
        }

        public void UpdateTimer(TimeSpan span)
        {
            _timerView.UpdateTimer(span);
        }

        private void SetDefaultState()
        {
            _checkGameObject.SetActive(false);
            _rewardBody.SetActive(true);
            _getBtn.gameObject.SetActive(false);
            _timerView.gameObject.SetActive(false);
        }
        
        private void SetCompletedState()
        {
            _checkGameObject.SetActive(true);
            _rewardBody.SetActive(false);
            _getBtn.gameObject.SetActive(true);
            _timerView.gameObject.SetActive(false);
        }
        
        private void SetTimeState()
        {
            _checkGameObject.SetActive(true);
            _rewardBody.SetActive(false);
            _getBtn.gameObject.SetActive(false);
            _timerView.gameObject.SetActive(true);
        }

        private void Notification()
        {
            OnPressGetBtnAction?.Invoke(_missionType);
        }
    }
}