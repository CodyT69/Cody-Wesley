using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit2D;


public class CollectableCoin : CollectableItem
{
    public CollectableCoin() 
    {
        _value = 5; // This value will be randomized when the object is awake
        _collectableType = CollectableType.Coin;
        _itemName = "coin";
    }

    private void Awake()
    {
        // Set the value to be within a random range, this is not allowed to be
        // called in the constructor
        _value = Random.Range(1, 8);
    }
}
