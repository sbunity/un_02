using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using Views.General;
using Enums;
using SO;
using Sounds;
using Values;

namespace Controllers.Scenes
{
    public abstract class AbstractSceneController : MonoBehaviour
    {
        [SerializeField] 
        private BalanceUpdater _balanceView;
        [SerializeField] 
        private SoundsController _soundsController;
        [SerializeField]
        private SceneSounds _sceneSounds;

        private MusicController _musicController;

        private void OnEnable()
        {
            Wallet.OnChangedMoney += UpdateMoneyCountText;
            
            _musicController = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicController>();
            
            _sceneSounds.SetAudioClip();
            
            Initialize();
            Subscribe();
            OnSceneEnable();
            
            UpdateMoneyCountText();
        }

        private void Start()
        {
            PlayMusic();
            OnSceneStart();
        }

        private void OnDisable()
        {
            Wallet.OnChangedMoney -= UpdateMoneyCountText;
            
            Unsubscribe();
            OnSceneDisable();
        }

        protected abstract void OnSceneEnable();
        protected abstract void OnSceneStart();
        protected abstract void OnSceneDisable();
        protected abstract void Initialize();
        protected abstract void Subscribe();
        protected abstract void Unsubscribe();

        protected void LoadScene(SceneType scene)
        {
            SetClickClip();
            
            StartCoroutine(DelayLoadScene(scene.ToString()));
        }

        protected void SetClickClip()
        {
            PlaySound(AudioNames.ClickClip);
        }

        protected void PlaySound(AudioNames name)
        { 
            _soundsController.TryPlaySound(GetAudioClip(name));
        }
        
        protected void PlayMusic()
        {
            AudioNames clipName = SceneManager.GetActiveScene().name == SceneType.Game.ToString()
                ? AudioNames.GameClip
                : AudioNames.MenuClip;

            _musicController.TryPlayMusic(GetAudioClip(clipName));
        }
        
        private AudioClip GetAudioClip(AudioNames name)
        {
            return _sceneSounds.GetAudioClipByName(name.ToString());
        }
        
        private void UpdateMoneyCountText()
        {
            if (_balanceView == null)
            {
                return;
            }

            _balanceView.UpdateText(Wallet.Money);
        }

        private IEnumerator DelayLoadScene(string sceneName)
        {
            yield return new WaitForSecondsRealtime(0.3f);

            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }

            SceneManager.LoadScene(sceneName);
        }
    }
}