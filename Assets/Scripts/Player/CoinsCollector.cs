using System;
using UnityEngine;

public class CoinsCollector : MonoBehaviour
{
    public event Action AmountChanged;

    private int _collectedCoins = 0;

    public int CollectedCoins => _collectedCoins;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Coin>())
        {
            Destroy(collision.gameObject);

            _collectedCoins++;

            AmountChanged?.Invoke();
        }
    }
}