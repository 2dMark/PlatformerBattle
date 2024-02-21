using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthAmount;
    [SerializeField] private GameObject _player;

    private Health _health;

    private void Start()
    {
        _health = _player.GetComponent<Health>();
        _health.AmountChanged += RefreshHealthBar;

        RefreshHealthBar();
    }

    private void OnDisable()
    {
        _health.AmountChanged -= RefreshHealthBar;
    }

    private void RefreshHealthBar() => _healthAmount.text = 
        $"HP: {_health.HealthAmount}/{_health.MaxHealthAmount}";

}
