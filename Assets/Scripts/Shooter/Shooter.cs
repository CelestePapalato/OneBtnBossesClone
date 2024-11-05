using GameFlow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    Projectile projectilePrefab;
    [SerializeField]
    Transform spawnPoint;

    [Header("Configuration")]
    [SerializeField]
    float fireRate;

    private void OnEnable()
    {
        GameState.Instance?.OnGameStart.AddListener(StartShooting);
        GameState.Instance?.OnGameEnd.AddListener(StopShooting);
    }

    private void OnDisable()
    {
        GameState.Instance?.OnGameStart?.RemoveListener(StartShooting);
        GameState.Instance?.OnGameEnd?.RemoveListener(StopShooting);
    }
    public void StartShooting()
    {
        InvokeRepeating(nameof(Fire), 0, fireRate);
    }

    private void Fire()
    {
        Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
    }

    public void StopShooting()
    {
        CancelInvoke();
    }

}
