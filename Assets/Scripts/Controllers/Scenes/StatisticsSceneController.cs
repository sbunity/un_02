using System.Collections.Generic;
using Enums;
using UnityEngine;
using Views.Statistics;
using Models.Scenes;
using UnityEngine.UI;

namespace Controllers.Scenes
{
    public class StatisticsSceneController : AbstractSceneController
    {
        [Space(5)] [Header("Views")] 
        [SerializeField]
        private WeeklyCountView _weeklyCountView;
        [SerializeField] 
        private DailyStaticsView _dailyStaticsView;
        [SerializeField] 
        private List<GraphView> _graphViews;

        [Space(5)] [Header("Buttons")] 
        [SerializeField]
        private Button _backBtn;
        
        private StatisticsSceneModel _model;
        
        protected override void OnSceneEnable()
        {
            UpdateGraphs();
            UpdateYMarks();
            UpdateGameStatistics();
        }

        protected override void OnSceneStart()
        {
            
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            _model = new StatisticsSceneModel();
        }

        protected override void Subscribe()
        {
            _backBtn.onClick.AddListener(OnPressBackBtn);
            _graphViews.ForEach(graph => graph.OnPressBtnAction += OnPressGraph);
        }

        protected override void Unsubscribe()
        {
            _backBtn.onClick.RemoveAllListeners();
            _graphViews.ForEach(graph => graph.OnPressBtnAction -= OnPressGraph);
        }

        private void UpdateYMarks()
        {
            _weeklyCountView.SetTexts(_model.GetYMarks());
        }

        private void UpdateGraphs()
        {
            List<float> lossFillAmounts = _model.GetLossFillAmounts();
            List<float> winFillAmounts = _model.GetWinFillAmounts();

            for (int i = 0; i < _graphViews.Count; i++)
            {
                _graphViews[i].SetSizeImages(winFillAmounts[i], lossFillAmounts[i]);
            }
        }

        private void UpdateGameStatistics()
        {
            _dailyStaticsView.SetDateText(_model.GetDate(_model.CurrentDayIndex));
            _dailyStaticsView.SetGameStatistics(_model.GetDailyGameStates());
            
            SetSelectedGraphState();
        }

        private void SetSelectedGraphState()
        {
            _graphViews.ForEach(view => view.SetState(false));
            
            _graphViews[_model.CurrentDayIndex].SetState(true);
        }

        private void OnPressBackBtn()
        {
            LoadMenuScene();
        }

        private void LoadMenuScene()
        {
            base.LoadScene(SceneType.Menu);
        }

        private void OnPressGraph(GraphView view)
        {
            int index = _graphViews.IndexOf(view);
            
            _model.ChangeSelectedGraphIndex(index);
            
            UpdateGameStatistics();
            SetSelectedGraphState();
        }
    }
}