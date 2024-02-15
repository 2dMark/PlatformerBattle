using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _startHealth;
    [SerializeField] private TMP_Text _healthAmount;

    private int _health;

    public int Health => _health;

    public int MaxHealth => _maxHealth;

    private void Start()
    {
        _health = _startHealth;

        if (_healthAmount != null)
            _healthAmount.text = $"{_health}/{_maxHealth}";
    }

    public void Heal(int number)
    {
        _health += number;

        if (_health > _maxHealth)
            _health = _maxHealth;

        if (_healthAmount != null)
            _healthAmount.text = $"{_health}/{_maxHealth}";
    }
}