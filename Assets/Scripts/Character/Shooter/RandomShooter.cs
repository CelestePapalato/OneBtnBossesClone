using UnityEngine;

public class RandomShooter : Shooter
{
    protected override void Fire()
    {
        Quaternion rotation = Quaternion.identity;
        float angle = Random.Range(0f, 360f);
        rotation = Quaternion.Euler(0f, 0f, angle);
        ObjectPool.Instance.GetObject(projectileTag, spawnPoint.position, rotation);
    }
}
