using System;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Statistics
{
    public class GraphView : MonoBehaviour
    {
        [SerializeField] 
        private Image _wonImage;
        [SerializeField] 
        private Image _lossImage;
        [SerializeField] 
        private Image _backImage;
        [SerializeField]
        private Button _btn;
        
        public event Action<GraphView> OnPressBtnAction;

        private void OnEnable()
        {
            _btn.onClick.AddListener(Notification);
        }

        private void OnDisable()
        {
            _btn.onClick.RemoveAllListeners();
        }

        public void SetSizeImages(float won, float loss)
        {
            _wonImage.fillAmount = won;
            _lossImage.fillAmount = loss;
        }

        public void SetState(bool value)
        {
            _backImage.gameObject.SetActive(value);
        }

        private void Notification()
        {
            OnPressBtnAction?.Invoke(this);
        }
    }
}