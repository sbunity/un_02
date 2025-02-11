using UnityEngine;
using UnityEngine.UI;

namespace Views.General
{
    public class IntegerTextUpdater : MonoBehaviour
    {
        [SerializeField] 
        private Text _text;

        public void UpdateText(int value)
        {
            _text.text = $"{value}";
        }
    }
}