using UnityEngine;

public class TargetShooter : Shooter
{
    [SerializeField]
    Transform target;

    protected override void Fire()
    {
        Quaternion rotation = Quaternion.identity;
        Vector2 direction = target.position - spawnPoint.position;
        float angle = Vector2.SignedAngle(Vector2.up, direction);
        rotation = Quaternion.Euler(0f, 0f, angle);
        ObjectPool.Instance.GetObject(projectileTag, spawnPoint.position, rotation);
    }
}
