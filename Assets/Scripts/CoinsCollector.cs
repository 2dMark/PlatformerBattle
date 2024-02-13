using TMPro;
using UnityEngine;

public class CoinsCollector : MonoBehaviour
{
    private const string CollectionTag = "Coin";

    [SerializeField] private TMP_Text _coinsAmont;

    private int _collectedCoins = 0;

    public int GetCollectedCoins => _collectedCoins;

    private void Start()
    {
        if (_coinsAmont != null)
            _coinsAmont.text = $"Coins: {_collectedCoins}";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(CollectionTag))
        {
            Destroy(collision.gameObject);

            _collectedCoins++;

            if (_coinsAmont != null)
                _coinsAmont.text = $"Coins: {_collectedCoins}";
        }
    }
}
