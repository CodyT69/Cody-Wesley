using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit2D;

public class CollectableController : MonoBehaviour, IDataPersister
{
    // Simple class to store items we collected
    [System.Serializable]
    class CollectedItem
    {
        private int _value = 1;
        private CollectableType _type;
        private string _itemName;

        public CollectedItem(int value, CollectableType type, string name)
        {
            this._value = value;
            this._type = type;
            this._itemName = name;
        }
        public CollectableType GetCollectableType()
        {
            return this._type;
        }
        public string GetItemName()
        {
            return this._itemName;
        }
        public int GetValue()
        {
            return _value;
        }
    }

    private List<CollectedItem> _collectedItems = new List<CollectedItem>();

    public void AddCollectable(CollectableItem item)
    {
        var newItem = new CollectedItem(item.GetValue(), item.GetCollectableType(), item.GetName());
        _collectedItems.Add(newItem);
        Debug.Log($"Item Collected, Name: {item.GetName()}, Type: {item.GetCollectableType()}, Value: {item.GetValue()}, Total count: {_collectedItems.Count}");

        // Update our UI to show our totals
        CollectableUI.Instance.UpdateValues(this);

        SortCollection();
        //PrintOutCollection();

    }

    public int GetTotalForCollectableType(CollectableType type)
    {
        int total = 0;
        foreach(var item in _collectedItems)
        {
            if(item.GetCollectableType() == type)
            {
                total += item.GetValue();
            }
        }
 
        return total;
    }

    private void SortCollection()
    {
        string msg;
        msg = "-----------------------\n";
        msg += "Collected Items:\n";

        // I'm seperating coins and gems for printing
        List<CollectedItem> coins = new List<CollectedItem>();
        List<CollectedItem> gems = new List<CollectedItem>();

        // Serperating coins and gems
        foreach (var item in _collectedItems)
        {
            if (item.GetCollectableType() == CollectableType.Coin)
            {
                coins.Add(item);
            }
            else if (item.GetCollectableType() == CollectableType.Gem)
            {
                gems.Add(item);
            }
        }

        // Sort coins by value in ascending order
        coins.Sort((x, y) => x.GetValue().CompareTo(y.GetValue()));

        // Sort gems by value in ascending order
        gems.Sort((x, y) => x.GetValue().CompareTo(y.GetValue()));

        // Print coins before gems
        foreach (var coin in coins)
        {
            msg += $"Coin value: {coin.GetValue()}\n";
        }

        // Print gems after coins
        foreach (var gem in gems)
        {
            msg += $"Gem: {gem.GetValue()}\n";
        }

        msg += "-----------------------\n";
        Debug.Log(msg);
    }



    // DATA PERSISTENT SYSTEM - To save collected items between zones (levels)

    // NOTE: We cannot make this field private, it seems to break the Persistent System and will
    //       not serialize/deserialize collected items between zones!
    //[SerializeField]
    public DataSettings dataSettings;


    void OnEnable()
    {
        PersistentDataManager.RegisterPersister(this);
    }

    void OnDisable()
    {
        PersistentDataManager.UnregisterPersister(this);
    }

    public DataSettings GetDataSettings()
    {
        return dataSettings;
    }

    public void SetDataSettings(string dataTag, DataSettings.PersistenceType persistenceType)
    {
        dataSettings.dataTag = dataTag;
        dataSettings.persistenceType = persistenceType;
    }

    public Data SaveData()
    {
        Debug.Log($"CollectableController.SaveData(), count={_collectedItems.Count}");
        return new Data<List<CollectedItem>>(_collectedItems);
    }

    public void LoadData(Data data)
    {
        Data<List<CollectedItem>> inventoryData = (Data<List<CollectedItem>>)data;
        Debug.Log($"CollectableController.LoadData(), count={inventoryData.value.Count}");
        foreach (var i in inventoryData.value)
            _collectedItems.Add(i);
    }
}
