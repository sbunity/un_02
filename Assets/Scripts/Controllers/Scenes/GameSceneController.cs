using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Controllers.Game;
using Views.Game;
using Models.Scenes;
using Enums;

using Views.General;

namespace Controllers.Scenes
{
    public class GameSceneController : AbstractSceneController
    {
        [Space(5)] [Header("Controllers")] 
        [SerializeField]
        private SpawnGameBallController _spawnGameBallController;

        [Space(5)] [Header("Views")] 
        [SerializeField]
        private BottomUIView _bottomUIView;
        [SerializeField] 
        private MissedBallsView _missedBallsView;
        [SerializeField] 
        private ResultPanel _resultPanel;
        [SerializeField] 
        private PanelView _pausePanel;
        [SerializeField] 
        private ConfirmationPanel _confirmationPanel;

        [Space(5)] [Header("Buttons")] 
        [SerializeField]
        private Button _pauseBtn;
        
        private GameSceneModel _model;
        
        protected override void OnSceneEnable()
        {
            ChangeState(GameState.Start);
            UpdateBet();
            SetStateBetBtns();
            UpdateWinBalance();
            SetActiveStartBtn();
        }

        protected override void OnSceneStart()
        {
            
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            _model = new GameSceneModel();
            
            _spawnGameBallController.Initialize();
        }

        protected override void Subscribe()
        {
            _pauseBtn.onClick.AddListener(OnPressPauseBtn);
            
            _bottomUIView.OnPressStartBtnAction += OnPressStartBtn;
            _bottomUIView.OnPressBetBtnAction += OnPressBetBtn;
            
            _spawnGameBallController.OnAddPointsAction += OnAddPoints;
            _spawnGameBallController.OnMissedMaxBallsAction += OnMissedMaxBalls;
            _spawnGameBallController.OnMissedBallAction += OnMissedBall;
        }

        protected override void Unsubscribe()
        {
            _pauseBtn.onClick.RemoveAllListeners();
            
            _bottomUIView.OnPressStartBtnAction -= OnPressStartBtn;
            _bottomUIView.OnPressBetBtnAction -= OnPressBetBtn;
            
            _spawnGameBallController.OnAddPointsAction -= OnAddPoints;
            _spawnGameBallController.OnMissedMaxBallsAction -= OnMissedMaxBalls;
            _spawnGameBallController.OnMissedBallAction -= OnMissedBall;
        }

        private void ChangeState(GameState state)
        {
            _bottomUIView.SetState(state);
            _spawnGameBallController.SetState(state);
        }

        private void OnPressStartBtn()
        {
            base.SetClickClip();
            
            ChangeState(GameState.Game);
            
            _model.SubtractBetFromBalance();
        }

        private void OnPressBetBtn(int direction)
        {
            base.SetClickClip();
            
            _model.ChangeBet(direction);
            
            SetStateBetBtns();
            UpdateBet();
        }

        private void OnAddPoints(int points)
        {
            base.PlaySound(AudioNames.GoalClip);
            
            _model.AddPoints(points);
            
            UpdateWinBalance();
        }

        private void OnMissedMaxBalls()
        {
            ChangeState(GameState.Finish);
            
            OpenResultPanel();
        }

        private void OnPressPauseBtn()
        {
            base.SetClickClip();
            
            Time.timeScale = 0;

            _pauseBtn.interactable = false;
            
            OpenPausePanel();
        }

        private void OnMissedBall(int value)
        {
            base.PlaySound(AudioNames.HitClip);
            
            UpdateMissedBallsView(value);
        }

        private void OnReceiveAnswerResultPanel(int answer)
        {
            _resultPanel.PressBtnAction -= OnReceiveAnswerResultPanel;

            switch (answer)
            {
                case 0:
                    base.LoadScene(SceneType.Menu);
                    break;
                case 1 :
                    base.LoadScene(SceneType.Game);
                    break;
            }
        }

        private void OnReceiveAnswerPausePanel(int answer)
        {
            base.SetClickClip();
            
            _pausePanel.PressBtnAction -= OnReceiveAnswerPausePanel;
            _pausePanel.gameObject.SetActive(false);
            
            switch (answer)
            {
                case 0:
                    StartCoroutine(DelayContinueGame());
                    break;
                case 1:
                    OpenConfirmationPanel(true);
                    break;
                case 2:
                    OpenConfirmationPanel(false);
                    break;
            }
        }

        private void OnReceiveAnswerConfirmationPanel(int answer, bool isRestart)
        {
            _confirmationPanel.PressBtnAction = null;

            switch (answer)
            {
                case 0 :
                    SceneType scene = isRestart ? SceneType.Game : SceneType.Menu;
                    base.LoadScene(scene);
                    break;
                case 1:
                    base.SetClickClip();
                    _confirmationPanel.gameObject.SetActive(false);
                    OpenPausePanel();
                    break;
            }
        }

        private void OpenResultPanel()
        {
            AudioNames name = _model.IsWin ? AudioNames.WinClip : AudioNames.LoseClip;
            
            base.PlaySound(name);
            
            _resultPanel.SetDescription(_model.IsWin);
            _resultPanel.SetReward(_model.Reward);
            _resultPanel.SetScore(_model.Points);
            _resultPanel.gameObject.SetActive(true);
            _resultPanel.PressBtnAction += OnReceiveAnswerResultPanel;
            
            _model.TryAddRewardToWallet();
            _model.TryCompletedMissions();
        }

        public void SetActiveStartBtn()
        {
            _bottomUIView.SetActiveStartBtn(_model.IsStartBtnActive);
        }

        private void OpenPausePanel()
        {
            _pausePanel.gameObject.SetActive(true);
            _pausePanel.PressBtnAction += OnReceiveAnswerPausePanel;
        }

        private void OpenConfirmationPanel(bool isRestart)
        {
            _confirmationPanel.SetBodySprite(isRestart);
            _confirmationPanel.PressBtnAction += answer => OnReceiveAnswerConfirmationPanel(answer, isRestart);
            _confirmationPanel.gameObject.SetActive(true);
        }

        private void UpdateBet()
        {
            _bottomUIView.SetBet(_model.Bet);
        }

        private void UpdateWinBalance()
        {
            _bottomUIView.UpdateWinBalance(_model.Points);
        }

        private void SetStateBetBtns()
        {
            _bottomUIView.SetStateBetBtns(_model.IsMinusBtnActive, _model.IsPlusBtnActive);
        }

        private void UpdateMissedBallsView(int value)
        {
            _missedBallsView.SetState(value);
        }

        private IEnumerator DelayContinueGame()
        {
            yield return new WaitForSecondsRealtime(1);

            Time.timeScale = 1;

            _pauseBtn.interactable = true;
        }
    }
}