using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit2D;

public class StoreUI : MonoBehaviour
{
    public UnityEngine.UI.Button buyHealthButton;
    public TMPro.TextMeshProUGUI coinText;

    private CollectableController _collectableController;
    private Damageable _damageable;

    const int HEALTH_COST = 15;

    private void Awake()
    {
        _collectableController = GameObject.Find("Ellen").GetComponent<CollectableController>();
        _damageable = GameObject.Find("Ellen").GetComponent<Damageable>();

        UpdateCoinTotal();
        buyHealthButton.interactable = CanBuyHealth();
    }

    private bool CanBuyHealth()
    {
        var coins = _collectableController.GetTotalForCollectableType(CollectableType.Coin);

        if (coins < 15 || _damageable.CurrentHealth == _damageable.startingHealth)
        {
            return false;
        }
        return true;
    }

    private void UpdateCoinTotal()
    {
        var coins = _collectableController.GetTotalForCollectableType(CollectableType.Coin);
        coinText.text = $"{coins}";
    }

    public void BuyHealth()
    {
        _collectableController.SpendCollectable(CollectableType.Coin, HEALTH_COST);
        _damageable.GainHealth(1);

        UpdateCoinTotal();
    }
}
