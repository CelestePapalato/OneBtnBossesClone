using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace GameFlow
{
    public class SessionData
    {
        public enum STATE { ONGOING, WON, LOST }

        public STATE state = STATE.ONGOING;
        public string level;
        public float lastTime = -1;
        private float newTime;
        public float NewTime
        {
            get => newTime;
            set
            {
                newTime = value;
                if ((newTime < lastTime || lastTime < 0) && state == STATE.WON)
                {
                    NewRecord = true;
                }
            }
        }
        public bool NewRecord { get; private set; } = false;

    }

    public class GameState : MonoBehaviour
    {
        public static GameState Instance { get; private set; }

        public enum STATE { GAME_ON_WAIT, GAME_START, GAME_RESTART, GAME_END }

        private STATE state;

        private SessionData sessionData;

        [SerializeField]
        Health playerHealth;
        [SerializeField]
        Health enemyHealth;

        public UnityEvent OnGameStart;
        public UnityEvent OnGameRestart;
        public UnityEvent OnGameEnd;
        public event Action<SessionData> OnGameSessionEnd; // el bool indica si fue una victoria o derrota y si super� su tiempo
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

            sessionData = CreateSessionData();
        }

        private SessionData CreateSessionData()
        {
            SessionData data = new SessionData();
            string level = SceneManager.GetActiveScene().name;
            data.level = level;
            if (PlayerPrefs.HasKey(level))
            {
                data.lastTime = PlayerPrefs.GetFloat(level);
            }
            return data;
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
            state = STATE.GAME_RESTART;
            OnGameRestart?.Invoke();
        }

        private void OnPlayerLost()
        {
            sessionData.state = SessionData.STATE.LOST;
            OnGameEnd?.Invoke();
            OnGameSessionEnd?.Invoke(sessionData);
            OnGameLost?.Invoke();
            Time.timeScale = 0f;
        }

        private void OnPlayerWon()
        {
            sessionData.state = SessionData.STATE.WON;
            UpdateTimeRecord();
            OnGameEnd?.Invoke();
            OnGameSessionEnd?.Invoke(sessionData);
            OnGameWon?.Invoke();
            Time.timeScale = 0f;
        }

        private bool UpdateTimeRecord()
        {
            float time = GameTimer.Instance.Timer;
            sessionData.NewTime = time;
            if(sessionData.NewRecord || !PlayerPrefs.HasKey(sessionData.level))
            {
                PlayerPrefs.SetFloat(sessionData.level, time);
                PlayerPrefs.Save();
                return true;
            }
            return false;
        }
    }
}