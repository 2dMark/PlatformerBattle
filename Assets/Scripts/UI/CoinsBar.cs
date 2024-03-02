using TMPro;
using UnityEngine;

public class CoinsBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsAmount;
    [SerializeField] private CoinsCollector _coinsCollector;

    private void Start()
    {
        _coinsCollector.AmountChanged += RefreshCoinsBar;

        RefreshCoinsBar();
    }

    private void OnDisable()
    {
        _coinsCollector.AmountChanged -= RefreshCoinsBar;
    }

    private void RefreshCoinsBar() => _coinsAmount.text =
        $"Coins: {_coinsCollector.CollectedCoins}";

}
