using GameFlow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent OnMoveAbilityInput;
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
        OnMoveAbilityInput?.Invoke();
        powerUp?.Use();
    }
}
