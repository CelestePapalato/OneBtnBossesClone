using UnityEngine;

public abstract class Healthbar : MonoBehaviour
{
    [SerializeField]
    protected Health health;

    private void Awake()
    {
        if(health == null) { Destroy(gameObject); return; }
    }

    private void OnEnable()
    {
        health.OnHealthUpdate += UpdateSlider;
    }

    private void OnDisable()
    {
        health.OnHealthUpdate -= UpdateSlider;
    }

    protected abstract void UpdateSlider(int current, int max);
}
