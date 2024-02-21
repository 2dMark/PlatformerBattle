using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(1)] private int _amount;
    [SerializeField, Min(1)] private int _maxAmount;

    public event Action AmountChanged;

    private void OnValidate()
    {
        if (_maxAmount < _amount)
            _amount = _maxAmount;
    }

    public int HealthAmount => _amount;

    public int MaxHealthAmount => _maxAmount;

    public void Heal(int number)
    {
        _amount += number;

        if (_amount > _maxAmount)
            _amount = _maxAmount;

        AmountChanged?.Invoke();
    }

    public void TakeDamage(int amount)
    {
        _amount -= amount;

        AmountChanged?.Invoke();
    }
}