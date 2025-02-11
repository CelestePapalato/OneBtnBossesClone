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
        public SessionData CurrentSessionData
        {
            get
            {
                SessionData copy = new SessionData();
                copy.state = sessionData.state;
                copy.level = sessionData.level;
                copy.lastTime = sessionData.lastTime;
                copy.NewTime = sessionData.NewTime;
                return copy;
            }
        }

        [SerializeField]
        Health playerHealth;
        [SerializeField]
        Health enemyHealth;

        public UnityEvent OnGameStart;
        public UnityEvent OnGameRestart;
        public UnityEvent OnGameEnd;
        public event Action<SessionData> OnGameSessionEnd; // el bool indica si fue una victoria o derrota y si superó su tiempo

        public Health PlayerHealth
        {
            get => playerHealth; 
            set
            {
                if(state != STATE.GAME_ON_WAIT || !value) { return; }
                if (playerHealth)
                {
                    playerHealth.OnDeath -= OnPlayerLost;
                }
                playerHealth = value;
                playerHealth.OnDeath += OnPlayerLost;
            }
        }
        public Health EnemyHealth
        {
            get => enemyHealth;
            set
            {
                if (state != STATE.GAME_ON_WAIT || !value) { return; }
                if (enemyHealth)
                {
                    enemyHealth.OnDeath -= OnPlayerWon;
                }
                enemyHealth = value;
                enemyHealth.OnDeath += OnPlayerWon;
            }
        }


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

            EnemyHealth = enemyHealth;
            PlayerHealth = playerHealth;
        }

        private void OnDestroy()
        {
            if(enemyHealth != null) { enemyHealth.OnDeath -= OnPlayerWon; }
            if(playerHealth != null) { playerHealth.OnDeath -= OnPlayerLost; }
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

        public void StartGame()
        {
            if (state == STATE.GAME_ON_WAIT)
            {
                Time.timeScale = 1f;
                state = STATE.GAME_START;
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
            state = STATE.GAME_END;
            sessionData.state = SessionData.STATE.LOST;
            OnGameEnd?.Invoke();
            OnGameSessionEnd?.Invoke(sessionData);
            Time.timeScale = 0f;
        }

        private void OnPlayerWon()
        {
            state = STATE.GAME_END;
            sessionData.state = SessionData.STATE.WON;
            UpdateTimeRecord();
            OnGameEnd?.Invoke();
            OnGameSessionEnd?.Invoke(sessionData);
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