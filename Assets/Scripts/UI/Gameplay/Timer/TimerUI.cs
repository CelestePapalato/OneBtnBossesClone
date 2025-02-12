using GameFlow;
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

            timer_ui.text = GameTimer.TimeToString(time);
        }
    }
}