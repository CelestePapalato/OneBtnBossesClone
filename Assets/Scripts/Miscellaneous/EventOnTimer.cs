using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnTimer : MonoBehaviour
{
    [SerializeField]
    float time;
    [SerializeField]
    bool repeat = false;

    public UnityEvent OnTimerEnd;

    private void OnEnable()
    {
        StartTimer();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }


    private void StartTimer()
    {
        if(repeat)
        {
            InvokeRepeating(nameof(CallEvent), time, time);
        }
        else
        {
            Invoke(nameof(CallEvent), time);
        }
    }

    private void CallEvent()
    {
        OnTimerEnd?.Invoke();
    }
}
