using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCone : EnemyAttack
{
    [SerializeField]
    string coneTag;
    [SerializeField]
    float attackRate;

    public override void Initialize(GameObject owner)
    {
        InvokeRepeating(nameof(Attack), attackRate, attackRate);
    }

    public override void Attack()
    {
        float angle = Random.Range(0f, 360f);
        Quaternion rotation = Quaternion.Euler(0F, 0f, angle);
        Vector3 point = CircularMovement.Instance.Center;
        ObjectPool.Instance.GetObject(coneTag, point, rotation);
    }

}
