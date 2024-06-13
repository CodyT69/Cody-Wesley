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

    public void AddCollectable(CollectableItem item)
    {
        Debug.Log($"Item Collected, Name: {item.GetName()}, Value: {item.GetValue()}");
        var newItem = new CollectedItem(item.GetValue(), item.GetCollectableType(), item.GetName());
        _collectedItems.Add(newItem);
        Debug.Log($"Item Collected, items count: {_collectedItems.Count}");
    }


    // DATA PERSISTENT SYSTEM - To save collected items between zones (levels)

    // NOTE: We cannot make this field private, it seems to break the Persistent System and will
    //       not serialize/deserialize collected items between zones!
    //[SerializeField]
    private DataSettings dataSettings;


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
