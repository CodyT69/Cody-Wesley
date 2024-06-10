using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    class CollectedItem
    {
        public int _value = 1;
        public CollectableType _type;
        public string _itemName;

        public CollectedItem(int value, CollectableType type, string name)
        {
            this._value = value;
            this._type = type;
            this._itemName = name;
        }
    }

    List<CollectedItem> _collectedItems = new List<CollectedItem>();
    int _coinValues;


    public void AddCollectable(CollectableItem item)
    {
        Debug.Log($"Item Collected, Name: {item.GetName()}, Value: {item.GetValue()}");
        var newItem = new CollectedItem(item.GetValue(), item.GetCollectableType(), item.GetName());
        _collectedItems.Add(newItem);
        Debug.Log($"Item Collected, items count: {_collectedItems.Count}");
    }
}
