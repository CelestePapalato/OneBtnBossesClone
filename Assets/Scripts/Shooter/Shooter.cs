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
    [SerializeField]
    bool randomizeDirection = false;

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
        Quaternion rotation = Quaternion.identity;

        if(randomizeDirection)
        {
            float angle = Random.Range(0f, 360f);
            rotation = Quaternion.Euler(0f, 0f, angle);
        }

        Instantiate(projectilePrefab, spawnPoint.position, rotation);
    }

    public void StopShooting()
    {
        CancelInvoke();
    }

}
