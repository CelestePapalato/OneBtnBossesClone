using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeedPowerUp : PowerUp
{
    [SerializeField]
    float speedMultiplier;
    [SerializeField]
    float activeTime;
    [SerializeField]
    float reloadingTime;

    public UnityEvent<float> OnChargeUpdate;

    [Header("Debug")]
    [SerializeField]
    private float m_charge = 0;

    public float Charge
    {
        get => m_charge;
        private set
        {
            m_charge = value;
            OnChargeUpdate?.Invoke(m_charge / 100);
        }
    }

    GameObject currentTarget;
    Health targetHealth;
    CircularMovement targetMovement;

    public bool FullCharge { get => m_charge == 100; }

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
        targetMovement.EnableDirectionChange(false);
        targetHealth.SetInvincibility(true);
        float time = activeTime;
        while(Charge > 0)
        {
            yield return null;
            time -= Time.deltaTime;
            Charge = Mathf.Lerp(0, 100, time/activeTime);
        }
        targetMovement.SpeedMultiplier = 1f;
        targetHealth.SetInvincibility(false);
        targetMovement.EnableDirectionChange(true);
        currentState = StartCoroutine(PowerUpReloading());
    }

    private IEnumerator PowerUpReloading()
    {
        float time = reloadingTime;
        while (Charge < 100)
        {
            yield return null;
            time -= Time.deltaTime;
            Charge = Mathf.Lerp(100, 0, time / reloadingTime);
        }
        currentState = null;
    }
}
