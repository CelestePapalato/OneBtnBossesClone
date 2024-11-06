using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    public abstract void Initialize(GameObject owner);
    public abstract void Attack();
}
