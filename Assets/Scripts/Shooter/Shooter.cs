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
    Transform target;
    [SerializeField]
    bool randomizeDirection = false;

    private void Start()
    {
        if (target)
        {
            randomizeDirection = false;
        }
    }

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
        else
        {
            Vector2 direction = target.position - spawnPoint.position;
            float angle = Vector2.SignedAngle(Vector2.up, direction);
            Debug.Log(angle);
            rotation = Quaternion.Euler(0f, 0f, angle);
        }

        Projectile projectile = Instantiate(projectilePrefab, spawnPoint.position, rotation);
    }

    public void StopShooting()
    {
        CancelInvoke();
    }

}
