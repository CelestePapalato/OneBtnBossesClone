using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObstacle : EnemyAttack
{
    [SerializeField]
    GameObject obstaclePrefab;
    [SerializeField]
    float waitTime;
    [SerializeField]
    float attackRate;

    public override void Initialize(GameObject owner)
    {
        InvokeRepeating(nameof(Attack), waitTime, attackRate);
    }

    public override void Attack()
    {
        Vector3 point = CircularMovement.Instance.GetRandomPoint();
        GameObject Instance = Instantiate(obstaclePrefab, point, Quaternion.identity);
        Instance.SetActive(true);
    }

}
