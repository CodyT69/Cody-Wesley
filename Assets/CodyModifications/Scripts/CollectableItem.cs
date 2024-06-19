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

    // Public attributes so that they can be edited in the Inspector
    public LayerMask layers;
    public AudioClip clip;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (layers.Contains(other.gameObject))
        {
            var cc = other.GetComponent<CollectableController>();
            cc.AddCollectable(this);
        }

        if (clip)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
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
