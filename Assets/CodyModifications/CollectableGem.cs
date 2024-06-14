using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableGem : CollectableItem
{
    public CollectableGem()
    {
        _value = 3; // This value will be randomized when the object is awake
        _collectableType = CollectableType.Gem;
        _itemName = "Gem";
    }

    private void Awake()
    {
        // Set the value to be within a random range, this is not allowed to be
        // called in the constructor
        _value = Random.Range(1, 3);
    }
}
