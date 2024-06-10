using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit2D;

public enum CollectableType
{
    Coin,
    Gem
}

public class CollectableItem : MonoBehaviour
{
    protected int _value = 1;
    protected CollectableType _collectableType;
    protected string _itemName;
    public LayerMask layers;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (layers.Contains(other.gameObject))
        {
            Debug.Log("Collect item");
            var cc = other.GetComponent<CollectableController>();
            cc.AddCollectable(this);
        }
    }
    public int GetValue()
    {
        return _value;
    }
    public CollectableType GetCollectableType()
    {
        return _collectableType;
    }
    
    public string GetName()
    {
        return _itemName;
    }
}
