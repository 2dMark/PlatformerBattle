using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Health))]

class Vampirism : Ability
{
    [SerializeField] private float _drainRadius = 5f;
    [SerializeField] private float _healthTransferPerSecond = 3f;
    [SerializeField] private float _actionTimeSeconds = 6f;
    [SerializeField] private float _cooldown = 10f;

    private Movement _movement;
    private Health _health;
    private float _actionTime;
    private Coroutine _abilityWork;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _health = GetComponent<Health>();
    }

    public override void Use()
    {
        if (_abilityWork == null)
            _abilityWork = StartCoroutine(Drain());
    }

    private void OnDisable()
    {
        StopCoroutine(_abilityWork);
    }

    private IEnumerator Drain()
    {
        WaitForSeconds cooldown = new(_cooldown);
        WaitForSeconds second = new(1f);

        _actionTime = _actionTimeSeconds;

        while (_actionTime-- > 0)
        {
            if (TryGetCloseTarget(out Health target))
                TransferHealth(target);

            yield return second;
        }

        yield return cooldown;

        _abilityWork = null;
    }

    private bool TryGetCloseTarget(out Health target)
    {
        RaycastHit2D[] enemies;
        List<Health> targets = new();

        target = null;

        enemies = Physics2D.CircleCastAll
            (transform.position, _drainRadius, Vector2.zero, 0, _movement.AttackableMask);

        if (enemies == null)
            return false;

        foreach (RaycastHit2D enemy in enemies)
            if (enemy.transform.gameObject.TryGetComponent(out Health health))
                if (health.IsAlive)
                    targets.Add(health);

        if (targets.Count == 0)
            return false;

        target = targets[0];

        for (int i = 1; i < targets.Count; i++)
            if ((targets[i].transform.position - transform.position).magnitude <
                (target.transform.position - transform.position).magnitude)
                target = targets[i];

        return true;
    }

    private void TransferHealth(Health target)
    {
        target.TakeDamage(_healthTransferPerSecond);
        _health.Heal(_healthTransferPerSecond);
    }
}