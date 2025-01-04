using GameFlow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent OnPauseInput;
    public PowerUp powerUp;

    private void OnEnable()
    {
        GameFlow.GameState.Instance?.OnGameStart.AddListener(InitializePowerUps);
    }

    private void OnDisable()
    {
        GameFlow.GameState.Instance?.OnGameStart?.RemoveListener(InitializePowerUps);
    }

    private void InitializePowerUps()
    {
        powerUp?.Initialize(gameObject);
    }

    private void OnMoveAbility()
    {
        if(Time.timeScale == 0f) { return; }
        powerUp?.Use();
    }

    private void OnPauseMenu()
    {
        OnPauseInput?.Invoke();
    }
}
