using UnityEngine.UI;

public class EnemyHealthbar : Healthbar
{
    Slider slider;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    protected override void UpdateSlider(int current, int max)
    {
        slider.value = (float)current / max;
    }
}
