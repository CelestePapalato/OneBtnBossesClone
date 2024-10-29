using System;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    public enum STATE { GAME_ON_WAIT, GAME_START, GAME_RESTART, GAME_END }

    private STATE state;

    public event Action OnGameStart;
    public event Action OnGameRestart;
    public event Action OnGameEnd;

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
        if (state == STATE.GAME_RESTART)
        {
            OnGameRestart?.Invoke();
        }
    }
}
