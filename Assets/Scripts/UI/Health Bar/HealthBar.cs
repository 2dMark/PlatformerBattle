using UnityEngine;

abstract class HealthBar : MonoBehaviour
{
    [SerializeField] protected Health _health;

    protected virtual void Awake()
    {
        RefreshData();
    }

    protected virtual void OnEnable()
    {
        _health.AmountChanged += RefreshData;
    }

    protected virtual void OnDisable()
    {
        _health.AmountChanged -= RefreshData;
    }

    protected virtual void RefreshData()
    { }
}