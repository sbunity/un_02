using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Views.Game
{
    public class StaticBallView : MonoBehaviour
    {
        [SerializeField] 
        private Text _text;
        [SerializeField] 
        private RectTransform _rectTransform;

        public void SetText(int count)
        {
            _text.text = count > 0 ? $"+{count}" : $"{count}";
        }

        public void StartAnim()
        {
            _text.gameObject.SetActive(true);
            
            AnimObject(1.5f);
        }

        private void FinishAnim()
        {
            _text.gameObject.SetActive(false);
            
            AnimObject(1);
        }

        private void AnimObject(float target)
        {
            Vector3 newScale = Vector3.one * target;

            if (target > 1)
            {
                _rectTransform.DOScale(newScale, 1).OnComplete(FinishAnim);
            }
            else
            {
                _rectTransform.DOScale(newScale, 1);
            }
        }
    }
}