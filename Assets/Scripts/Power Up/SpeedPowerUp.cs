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
    [SerializeField]
    [Tooltip("Carga mínima necesaria para volver a usar la habilidad " +
        "si el jugador ha usado la carga por completo")]
    float minChargeForReactivating = 25f;

    public UnityEvent<float> OnChargeUpdate;

    [Header("Debug")]
    [SerializeField]
    private float m_charge = 100;

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
    public bool CanUse { get; private set; } = true;
    private bool active = false;

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
        Charge = m_charge;
        StartCoroutine(PowerUpReloading());
    }

    public override void Use()
    {
        if (!currentTarget || !CanUse)
        {
            return;
        }
        if (!active)
        {
            StartCoroutine(EnablePowerUp());
            return;
        }
        active = false;
    }

    private IEnumerator EnablePowerUp()
    {
        if (!targetMovement)
        {
            yield break;
        }
        active = true;
        targetMovement.EnableDirectionChange(false);
        targetMovement.SpeedMultiplier = speedMultiplier;
        targetHealth.SetInvincibility(true);
        float time = Charge * activeTime / 100;
        while (Charge > 0 && active != false)
        {
            yield return null;
            time -= Time.deltaTime;
            Charge = Mathf.Lerp(0, 100, time / activeTime);
        }
        targetMovement.SpeedMultiplier = 1f;
        targetHealth.SetInvincibility(false);
        targetMovement.EnableDirectionChange(true);
        active = false;
        if (Charge <= 0)
        {
            CanUse = false;
        }
        StartCoroutine(PowerUpReloading());
    }

    private IEnumerator PowerUpReloading()
    {
        float time = reloadingTime - (Charge * reloadingTime / 100f);

        while (!FullCharge && !active)
        {
            yield return null;
            time -= Time.deltaTime;
            Charge = Mathf.Lerp(100, 0, time / reloadingTime);
            if (!CanUse)
            {
                CanUse = Charge >= minChargeForReactivating;
            }
        }
    }
}
