using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    private int health;

    public UnityAction<int, int> OnDamaged;
    public UnityAction OnDeath;

    private void Start()
    {
        health = maxHealth;
    }

    private void Damage()
    {
        health = Mathf.Max(health-1, 0);
        OnDamaged?.Invoke(health, maxHealth);
        if(health <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage();
    }
}
