using System.Collections;
using UnityEngine;
using TMPro;
using System;

namespace GameFlow
{ 
    public class GameTimer : MonoBehaviour
    {
        public static GameTimer Instance;

        private float timer;
        private bool active = false;

        private Coroutine timerCoroutine;

        public float Timer { get => timer; }

        public event Action OnTimerStart;
        public event Action<float> OnTimerUpdate;
        public event Action OnTimerEnd;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }

        private void OnEnable()
        {
            GameState.Instance.OnGameStart += StartTimer;
            GameState.Instance.OnGameEnd += EndTimer;
            GameState.Instance.OnGameRestart += EndTimer;
        }

        private void OnDisable()
        {
            GameState.Instance.OnGameStart -= StartTimer;
            GameState.Instance.OnGameEnd -= EndTimer;
            GameState.Instance.OnGameRestart -= EndTimer;
        }

        private IEnumerator EnableTimer()
        {
            active = true;
            OnTimerStart?.Invoke();
            while (active)
            {
                yield return null;
                timer += Time.deltaTime;
                OnTimerUpdate?.Invoke(timer);
            }
            active = false;
            OnTimerEnd?.Invoke();
        }

        private void StartTimer()
        {
            EndTimer();
            timer = 0f;
            timerCoroutine = StartCoroutine(EnableTimer());
        }

        private void EndTimer()
        {
            if (timerCoroutine == null) { return; }
            StopCoroutine(timerCoroutine);
        }
    }
}