using UnityEngine;
using UnityEngine.UI;

class SlideHealthBar : HealthBar
{
    [SerializeField] protected Slider _slider;

    protected override void RefreshData() =>
        _slider.value = _health.Amount / _health.MaxAmount;
}