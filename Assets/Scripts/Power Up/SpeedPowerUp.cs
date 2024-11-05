using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpeedPowerUp : PowerUp
{
    [SerializeField]
    float speedMultiplier;
    [SerializeField]
    float activeTime;
    [SerializeField]
    float reloadingTime;

    [Header("Debug")]
    [SerializeField]
    private float charge = 0;

    GameObject currentTarget;
    Health targetHealth;
    Collider2D targetCollider;
    CircularMovement targetMovement;

    public bool FullCharge { get => charge == 100; }

    Coroutine currentState;

    public override void Initialize(GameObject target)
    {
        CircularMovement movementComponent = target.GetComponentInChildren<CircularMovement>();
        Health healthComponent = target.GetComponentInChildren<Health>();
        if (!movementComponent || !healthComponent)
        {
            currentTarget = null;
            return;
        }
        currentTarget = target;
        targetMovement = movementComponent;
        targetHealth = healthComponent;
        targetCollider = healthComponent.GetComponent<Collider2D>();
        currentState = StartCoroutine(PowerUpReloading());
    }

    public override void Use()
    {
        if (!currentTarget || currentState != null)
        {
            return;
        }
        currentState = StartCoroutine(EnablePowerUp());
    }

    private IEnumerator EnablePowerUp()
    {
        if (!FullCharge || !targetMovement) { yield break; }
        targetMovement.SpeedMultiplier = speedMultiplier;
        targetMovement.SetDirectionChange(false);
        SetInvincibility(true);
        float time = activeTime;
        while(charge > 0)
        {
            yield return null;
            time -= Time.deltaTime;
            charge = Mathf.Lerp(0, 100, time/activeTime);
        }
        targetMovement.SpeedMultiplier = 1f;
        SetInvincibility(false);
        targetMovement.SetDirectionChange(true);
        currentState = StartCoroutine(PowerUpReloading());
    }

    private void SetInvincibility(bool active)
    {
        targetCollider.enabled = !active;
    }

    private IEnumerator PowerUpReloading()
    {
        float time = reloadingTime;
        while (charge < 100)
        {
            yield return null;
            time -= Time.deltaTime;
            charge = Mathf.Lerp(100, 0, time / reloadingTime);
        }
        currentState = null;
    }
}
