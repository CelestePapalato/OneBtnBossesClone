using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private float invincibilityTimer;

    private int health;
    private bool invincible = false;
    Collider2D hurtbox;

    public UnityAction<int, int> OnDamaged;
    public UnityAction OnDeath;

    private void Start()
    {
        health = maxHealth;
        hurtbox = GetComponent<Collider2D>();
    }

    private void Damage()
    {
        if(invincible) { return; }
        health = Mathf.Max(health-1, 0);
        OnDamaged?.Invoke(health, maxHealth);
        if(health <= 0)
        {
            OnDeath?.Invoke();
        }
        StartCoroutine(InvincibilityEnabler());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage();
    }

    private IEnumerator InvincibilityEnabler()
    {
        hurtbox.enabled = false;
        invincible = true;
        yield return new WaitForSeconds(invincibilityTimer);
        hurtbox.enabled = true;
        invincible = false;
    }

}
