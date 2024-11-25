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
        public event Action<bool, bool> OnGameSessionEnd; // el bool indica si fue una victoria o derrota y si superó su tiempo
        public UnityEvent OnGameWon;
        public UnityEvent OnGameLost;

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
            playerHealth.OnDeath += OnPlayerLost;
            enemyHealth.OnDeath += OnPlayerWon;
        }

        private void OnDisable()
        {
            playerHealth.OnDeath -= OnPlayerLost;
            enemyHealth.OnDeath -= OnPlayerWon;
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

        private void OnPlayerLost()
        {
            OnGameEnd?.Invoke();
            OnGameSessionEnd?.Invoke(false, false);
            OnGameLost?.Invoke();
            Time.timeScale = 0f;
        }

        private void OnPlayerWon()
        {
            OnGameEnd?.Invoke();
            OnGameSessionEnd?.Invoke(true, UpdateTimeRecord());
            OnGameWon?.Invoke();
            Time.timeScale = 0f;
        }

        private bool UpdateTimeRecord()
        {
            float time = GameTimer.Instance.Timer;
            string playerPrefKey = SceneManager.GetActiveScene().name; 
            if(time < PlayerPrefs.GetFloat(playerPrefKey) || !PlayerPrefs.HasKey(playerPrefKey))
            {
                PlayerPrefs.SetFloat(playerPrefKey, time);
                PlayerPrefs.Save();
                return true;
            }
            return false;
        }

        public void ExitLevel()
        {
            SceneManager.LoadScene("LEVEL SELECTION");
        }
    }
}