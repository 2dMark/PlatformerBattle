using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _startHealth;
    [SerializeField] private TMP_Text _healthAmount;

    private int _health;

    private void Start()
    {
        _health = _startHealth;

        if (_healthAmount != null)
            _healthAmount.text = $"{_health}/{_maxHealth}";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealingPotion healingPotion))
        {
            if (_health == _maxHealth)
                return;

            _health += healingPotion.GetRecoverableHealth;

            if (_health > _maxHealth)
                _health = _maxHealth;

            Destroy(collision.gameObject);

            if (_healthAmount != null)
                _healthAmount.text = $"{_health}/{_maxHealth}";
        }
    }
}