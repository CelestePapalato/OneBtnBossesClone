using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public abstract void Initialize(GameObject target);
    public abstract void Use();
}
