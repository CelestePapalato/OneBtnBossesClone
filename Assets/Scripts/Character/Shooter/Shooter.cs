using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    string projectileTag;
    [SerializeField]
    Transform spawnPoint;

    [Header("Configuration")]
    [SerializeField]
    float fireRate;
    [SerializeField]
    Transform target;
    [SerializeField]
    bool randomizeDirection = false;

    private void Start()
    {
        if (target)
        {
            randomizeDirection = false;
        }
        StartShooting();
    }

    public void StartShooting()
    {
        CancelInvoke();
        InvokeRepeating(nameof(Fire), 0, fireRate);
    }

    private void Fire()
    {
        Quaternion rotation = Quaternion.identity;

        if(randomizeDirection)
        {
            float angle = Random.Range(0f, 360f);
            rotation = Quaternion.Euler(0f, 0f, angle);
        }
        else
        {
            Vector2 direction = target.position - spawnPoint.position;
            float angle = Vector2.SignedAngle(Vector2.up, direction);
            rotation = Quaternion.Euler(0f, 0f, angle);
        }

        ObjectPool.Instance.GetObject(projectileTag, spawnPoint.position, rotation);
    }

    public void StopShooting()
    {
        CancelInvoke();
    }

}
