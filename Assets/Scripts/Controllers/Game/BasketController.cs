using UnityEngine;
using UnityEngine.EventSystems;

namespace Controllers.Game
{
    public class BasketController : MonoBehaviour, IDragHandler
    {
        [SerializeField]
        private RectTransform _hoopRect;
        [SerializeField] 
        private RectTransform _backHoodRect;
        [SerializeField]
        private RectTransform _canvasRect;

        private float minX, maxX;

        void Start()
        {
            SetMovementBounds();
        }

        private void SetMovementBounds()
        {
            float halfWidth = _hoopRect.rect.width/2;

            minX = -_canvasRect.rect.width / 2f + halfWidth;

            maxX = _canvasRect.rect.width / 2f - halfWidth;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, eventData.position,
                eventData.pressEventCamera, out localPoint);
            
            float clampedX = Mathf.Clamp(localPoint.x, minX, maxX);
            
            _hoopRect.anchoredPosition = new Vector2(clampedX, _hoopRect.anchoredPosition.y);
            _backHoodRect.anchoredPosition =
                new Vector2(_hoopRect.anchoredPosition.x, _backHoodRect.anchoredPosition.y);
        }
    }
}