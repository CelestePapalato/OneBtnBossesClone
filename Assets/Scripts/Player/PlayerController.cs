using GameFlow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent OnMoveAbilityInput;

    private void OnMoveAbility()
    {
        OnMoveAbilityInput?.Invoke();
    }
}
