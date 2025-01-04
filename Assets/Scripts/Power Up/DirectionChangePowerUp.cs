using UnityEngine;

public class DirectionChangePowerUp : PowerUp
{
    CircularMovement targetMovement;
    public override void Initialize(GameObject target)
    {
        targetMovement = target.GetComponentInChildren<CircularMovement>();
    }

    public override void Use()
    {
        targetMovement?.ChangeDirection();
    }
}
