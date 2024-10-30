using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameUI
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text timer_ui;

        private void OnEnable()
        {
            GameFlow.GameTimer.Instance.OnTimerUpdate += UpdateTimerUI;
        }

        private void OnDisable()
        {
            GameFlow.GameTimer.Instance.OnTimerUpdate += UpdateTimerUI;
        }

        private void UpdateTimerUI(float time)
        {
            if (!timer_ui) { return; }
            int minutes = (int)time / 60;
            float seconds = time % 60;

            timer_ui.text = minutes + " : " + seconds.ToString("0.00");
        }
    }
}