using UnityEngine;

public class PlayerHealthbar : Healthbar
{
    [SerializeField]
    Animator healthbar;
    [SerializeField]
    string damageAnimationTrigger;

    Animator[] healthpoints;

    int max;
    int current;

    public int Current { get => current; }

    void Awake()
    {
        max = health.MaxHealth;
        current = max;
        healthpoints = new Animator[max];
        for (int i = 0; i < health.MaxHealth; i++)
        {
            healthpoints[i] = Instantiate(healthbar, transform);
        }
    }

    protected override void UpdateSlider(int current, int max)
    {
        if(current < this.current)
        {
            healthpoints[this.current - 1].SetTrigger(damageAnimationTrigger);
            this.current = current;
        }
    }
}
