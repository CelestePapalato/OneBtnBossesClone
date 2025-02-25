using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    [SerializeField]
    protected string projectileTag;
    [SerializeField]
    protected Transform spawnPoint;

    [Header("Configuration")]
    [SerializeField]
    float fireRate;

    private void Start()
    {
        StartShooting();
    }

    public void StartShooting()
    {
        CancelInvoke();
        InvokeRepeating(nameof(Fire), 0, fireRate);
    }

    protected abstract void Fire();

    public void StopShooting()
    {
        CancelInvoke();
    }

}
