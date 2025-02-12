using System;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Menu
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] 
        private Text _timerText;

        public void UpdateTimer(TimeSpan span)
        {
            _timerText.text = $"{span.Hours}.{span.Minutes}.{span.Seconds}";
        }
    }
}