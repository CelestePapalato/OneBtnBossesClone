using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace GameFlow
{
    public class GameState : MonoBehaviour
    {
        public static GameState Instance { get; private set; }

        public enum STATE { GAME_ON_WAIT, GAME_START, GAME_RESTART, GAME_END }

        private STATE state;

        [SerializeField]
        Health playerHealth;
        [SerializeField]
        Health enemyHealth;

        public UnityEvent OnGameStart;
        public UnityEvent OnGameRestart;
        public UnityEvent OnGameEnd;
        public event Action<bool, bool> OnGameSessionEnd; // el bool indica si fue una victoria o derrota y si super� su tiempo

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            state = STATE.GAME_ON_WAIT;
            Time.timeScale = 0f;
        }

        private void OnEnable()
        {
            playerHealth.OnDeath += OnGameLost;
            enemyHealth.OnDeath += OnGameWon;
        }

        private void OnDisable()
        {
            playerHealth.OnDeath -= OnGameLost;
            enemyHealth.OnDeath -= OnGameWon;
        }

        public void StartGame()
        {
            if (state == STATE.GAME_ON_WAIT)
            {
                Time.timeScale = 1f;
                OnGameStart?.Invoke();
            }
        }

        public void RestartGame()
        {
            OnGameRestart?.Invoke();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnGameLost()
        {
            OnGameEnd?.Invoke();
            OnGameSessionEnd?.Invoke(false, false);
            Time.timeScale = 0f;
        }

        private void OnGameWon()
        {
            OnGameEnd?.Invoke();
            OnGameSessionEnd?.Invoke(true, UpdateTimeRecord());
            Time.timeScale = 0f;
        }

        private bool UpdateTimeRecord()
        {
            float time = GameTimer.Instance.Timer;
            if(time < PlayerPrefs.GetFloat("RecordTime") || !PlayerPrefs.HasKey("RecordTime"))
            {
                PlayerPrefs.SetFloat("RecordTime", time);
                PlayerPrefs.Save();
                return true;
            }
            return false;
        }
    }
}