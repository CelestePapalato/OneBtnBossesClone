using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public PlayerLoadout loadout;

    [SerializeField]
    private Transform playerPivot;

    public UnityEvent OnPauseInput;

    private PowerUp powerUp;

    private void Awake()
    {
        InstantiateLoadout();
    }

    private void Start()
    {
        InitializePowerUps();
    }

    private void InstantiateLoadout()
    {
        if (loadout == null) { return; }
        powerUp = Instantiate(loadout.PowerUp, playerPivot.transform);
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
