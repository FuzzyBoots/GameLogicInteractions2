using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] private List<InventoryItem> _inventoryItems;

    private void Start()
    {
        _inventoryItems = new List<InventoryItem>();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddItem(InventoryItem item)
    {
        Debug.Log($"Inventorying {item.ItemName}");
        if (!_inventoryItems.Any<InventoryItem>(invItem => invItem.ItemName == item.ItemName))
        {
            _inventoryItems.Add(item);
        }
    }

    public void RemoveItem(InventoryItem item)
    {
        InventoryItem foundItem = _inventoryItems.FirstOrDefault(invItem => invItem.ItemName == item.ItemName);
        if (foundItem != null)
        {
            _inventoryItems.Remove(foundItem);
        }
    }

    public bool HasItem(ItemEnums itemEnum)
    {
        return _inventoryItems.Any(item => item.ItemEnum == itemEnum);
    }
}
