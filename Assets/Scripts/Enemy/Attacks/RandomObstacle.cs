using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObstacle : EnemyAttack
{
    [SerializeField]
    GameObject obstaclePrefab;
    [SerializeField]
    float waitTime;

    public override void Initialize(GameObject owner)
    {
        StartCoroutine(AttackSequence());
    }

    public override void Attack()
    {
        Vector3 point = CircularMovement.Instance.GetRandomPoint();
        GameObject Instance = Instantiate(obstaclePrefab, point, Quaternion.identity);
        Instance.SetActive(true);
    }

    IEnumerator AttackSequence()
    {
        yield return new WaitForSeconds(waitTime);
        Attack();
    }
}
