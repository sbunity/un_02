using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Views.Menu;
using Enums;
using Models.Scenes;

namespace Controllers.Scenes
{
    public class MenuSceneController : AbstractSceneController
    {
        [Space(5)] [Header("Views")] 
        [SerializeField]
        private List<MissionView> _missionViews;
        [SerializeField] 
        private RewardPanel _rewardPanel;

        [Space(5)] [Header("Buttons")] 
        [SerializeField]
        private Button _settingsBtn;
        [SerializeField] 
        private Button _statisticsBtn;
        [SerializeField] 
        private Button _gameBtn;
        
        private MenuSceneModel _model;
        
        protected override void OnSceneEnable()
        {
            UpdateStateMissions();
        }

        protected override void OnSceneStart()
        {
            StartFirstMissionTimer();
            StartSecondMissionTimer();
            StartThirdMissionTimer();
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            _model = new MenuSceneModel();
        }

        protected override void Subscribe()
        {
            _settingsBtn.onClick.AddListener(OnPressSettingsBtn);
            _statisticsBtn.onClick.AddListener(OnPressStatisticsBtn);
            _gameBtn.onClick.AddListener(OnPressGameBtn);
            
            _missionViews.ForEach(mission => mission.OnPressGetBtnAction += OnPressGetBtn);
        }

        protected override void Unsubscribe()
        {
            _settingsBtn.onClick.RemoveAllListeners();
            _statisticsBtn.onClick.RemoveAllListeners();
            _gameBtn.onClick.RemoveAllListeners();
            
            _missionViews.ForEach(mission => mission.OnPressGetBtnAction -= OnPressGetBtn);
        }

        private void UpdateStateMissions()
        {
            UpdateFirstMission();
            UpdateSecondMission();
            UpdateThirdMission();
        }

        private void UpdateFirstMission()
        {
            _missionViews[0].SetState(_model.FirstMissionState);

            if (_model.FirstMissionState == MissionState.Time)
            {
                _missionViews[0].UpdateTimer(_model.FirstMissionTimeSpan);
            }
            else
            {
                CancelInvoke(nameof(UpdateFirstMission));
            }
        }
        
        private void UpdateSecondMission()
        {
            _missionViews[1].SetState(_model.SecondMissionState);
            
            if (_model.SecondMissionState == MissionState.Time)
            {
                _missionViews[1].UpdateTimer(_model.SecondMissionTimeSpan);
            }
            else
            {
                CancelInvoke(nameof(UpdateSecondMission));
            }
        }
        
        private void UpdateThirdMission()
        {
            _missionViews[2].SetState(_model.ThirdMissionState);
            
            if (_model.ThirdMissionState == MissionState.Time)
            {
                _missionViews[2].UpdateTimer(_model.ThirdMissionTimeSpan);
            }
            else
            {
                CancelInvoke(nameof(UpdateThirdMission));
            }
        }

        private void OnPressSettingsBtn()
        {
            base.LoadScene(SceneType.Settings);
        }

        private void OnPressStatisticsBtn()
        {
            base.LoadScene(SceneType.Statistics);
        }

        private void OnPressGameBtn()
        {
            base.LoadScene(SceneType.Game);
        }

        private void OnPressGetBtn(MissionType type)
        {
            OpenRewardPanel(type);
        }

        private void StartFirstMissionTimer()
        {
            InvokeRepeating(nameof(UpdateFirstMission), 0, 1);
        }
        
        private void StartSecondMissionTimer()
        {
            InvokeRepeating(nameof(UpdateSecondMission), 0, 1);
        }
        
        private void StartThirdMissionTimer()
        {
            InvokeRepeating(nameof(UpdateThirdMission), 0, 1);
        }

        private void OpenRewardPanel(MissionType type)
        {
            int reward = _model.GetRewardMission(type);
            
            _rewardPanel.UpdateReward(reward);
            _rewardPanel.PressBtnAction += answer => OnReceiveAnswerRewardPanel(type);
            _rewardPanel.gameObject.SetActive(true);
        }

        private void OnReceiveAnswerRewardPanel(MissionType type)
        {
            _rewardPanel.PressBtnAction = null;
            
            _rewardPanel.gameObject.SetActive(false);
            
            _model.ChangeMissionStateToTime(type);

            switch (type)
            {
                case MissionType.First:
                    StartFirstMissionTimer();
                    break;
                case MissionType.Second:
                    StartSecondMissionTimer();
                    break;
                case MissionType.Third:
                    StartThirdMissionTimer();
                    break;
            }
        }
    }
}