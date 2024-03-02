using TMPro;
using UnityEngine;

class TextHealthBar : HealthBar
{
    [SerializeField] protected TMP_Text _text;

    protected override void RefreshData()
    {
        if (_health.IsAlive)
            _text.text = $"< {_health.Amount} / {_health.MaxAmount} >";
        else
            _text.text = $"< Dead >";
    }
}