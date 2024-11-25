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
    private bool Invincible
    {
        get => invincible;

        set
        {
            invincible = value;
            if (invincible)
            {
                OnInvincibilityStart?.Invoke();
            }
            else
            {
                OnInvincibilityEnd?.Invoke();
            }
        }
    }

    Collider2D hurtbox;

    public UnityAction<int, int> OnDamaged;
    public UnityAction OnDeath;

    public UnityEvent OnInvincibilityStart;
    public UnityEvent OnInvincibilityEnd;

    bool invincibilityCoroutine = false;

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
        invincibilityCoroutine = true;
        hurtbox.enabled = false;
        Invincible = true;
        yield return new WaitForSeconds(invincibilityTimer);
        invincibilityCoroutine = false;
        hurtbox.enabled = true;
        Invincible = false;
    }

    public void SetInvincibility(bool value)
    {
        if (value)
        {
            StopAllCoroutines();
            invincibilityCoroutine = false;
            hurtbox.enabled = false;
            Invincible = value;
        }
        else if (!value && !invincibilityCoroutine)
        {
            hurtbox.enabled = true;
            Invincible = value;
        }
    }

}
