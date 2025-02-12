using Enums;
using UnityEngine;
using UnityEngine.UI;

using Views.General;
using RateUs;

namespace Controllers.Scenes
{
    public class SettingsSceneController : AbstractSceneController
    {
        [Space(5)] [Header("Views")] 
        [SerializeField]
        private GameObject _mainPanelGo;
        [SerializeField]
        private PanelView _privacyPanel;
        [SerializeField] 
        private PanelView _termsPanel;
        [SerializeField]
        private PanelView _confirmationPanel;

        [Space(5)] [Header("Buttons")] 
        [SerializeField]
        private Button _notificationBtn;
        [SerializeField] 
        private Button _privacyBtn;
        [SerializeField] 
        private Button _termsBtn;
        [SerializeField] 
        private Button _rateUsBtn;
        [SerializeField] 
        private Button _clearDataBtn;
        [SerializeField] 
        private Button _backBtn;
        
        private string key = "";
        
        protected override void OnSceneEnable()
        {
            OpenMainPanel();
        }

        protected override void OnSceneStart()
        {
            
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            
        }

        protected override void Subscribe()
        {
            _privacyBtn.onClick.AddListener(OnPressPrivacyBtn);
            _termsBtn.onClick.AddListener(OnPressTermsBtn);
            _rateUsBtn.onClick.AddListener(OnPressRateUsBtn);
            _clearDataBtn.onClick.AddListener(OnPressClearDataBtn);
            _backBtn.onClick.AddListener(OnPressBackBtn);
        }

        protected override void Unsubscribe()
        {
            _privacyBtn.onClick.RemoveAllListeners();
            _termsBtn.onClick.RemoveAllListeners();
            _rateUsBtn.onClick.RemoveAllListeners();
            _clearDataBtn.onClick.RemoveAllListeners();
            _backBtn.onClick.RemoveAllListeners();
        }

        private void SetActiveMainPanel(bool value)
        {
            _mainPanelGo.SetActive(value);
        }

        private void OpenMainPanel()
        {
            SetActiveMainPanel(true);
        }

        private void CloseMainPanel()
        {
            SetActiveMainPanel(false);
        }

        private void OnPressNotificationBtn()
        {
            
        }

        private void OnPressPrivacyBtn()
        {
            CloseMainPanel();
            OpenPrivacyPanel();
        }

        private void OnPressTermsBtn()
        {
            CloseMainPanel();
            OpenTermsPanel();
        }

        private void OnPressRateUsBtn()
        {
            OpenRateUs();
        }

        private void OnPressClearDataBtn()
        {
            OpenConfirmationPanel();
        }

        private void OnPressBackBtn()
        {
            OpenMenuScene();
        }

        private void OpenPrivacyPanel()
        {
            _privacyPanel.PressBtnAction += OnReceiveAnswerPrivacyPanel;
            _privacyPanel.gameObject.SetActive(true);
        }

        private void OpenTermsPanel()
        {
            _termsPanel.PressBtnAction += OnReceiveAnswerTermsPanel;
            _termsPanel.gameObject.SetActive(true);
        }

        private void OpenRateUs()
        {
#if UNITY_ANDROID
            key = Consts.KeyAndroid;
#elif UNITY_IOS
            key = Consts.KeyIOS;
#endif
            if(key == "") return;
            
            Application.OpenURL(PlayerPrefs.GetString(key));
        }

        private void OpenConfirmationPanel()
        {
            _confirmationPanel.PressBtnAction += OnReceiveAnswerConfirmationPanel;
            _confirmationPanel.gameObject.SetActive(true);
        }

        private void OpenMenuScene()
        {
            base.LoadScene(SceneType.Menu);
        }

        private void OnReceiveAnswerPrivacyPanel(int answer)
        {
            _privacyPanel.PressBtnAction -= OnReceiveAnswerPrivacyPanel;
            _privacyPanel.gameObject.SetActive(false);
            OpenMainPanel();
        }

        private void OnReceiveAnswerTermsPanel(int answer)
        {
            _termsPanel.PressBtnAction -= OnReceiveAnswerTermsPanel;
            _termsPanel.gameObject.SetActive(false);
            OpenMainPanel();
        }

        private void OnReceiveAnswerConfirmationPanel(int answer)
        {
            _confirmationPanel.PressBtnAction -= OnReceiveAnswerConfirmationPanel;
            _confirmationPanel.gameObject.SetActive(false);

            if (answer == 1)
            {
                ClearData();
            }
        }

        private void ClearData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}