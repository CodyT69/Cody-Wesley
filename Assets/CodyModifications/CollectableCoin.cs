using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit2D;


public class CollectableCoin : CollectableItem
{
    public CollectableCoin() 
    {
        _value = 5;
        _collectableType = CollectableType.Coin;
        _itemName = "coin";
    }
}
