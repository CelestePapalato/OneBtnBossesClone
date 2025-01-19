using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyAttack[] attacks;

    void Start()
    {
        attacks = GetComponentsInChildren<EnemyAttack>();
        InitializeAttacks();
    }


    void InitializeAttacks()
    {
        foreach (EnemyAttack attack in attacks)
        {
            attack.Initialize(gameObject);
        }
    }
}
