using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] string _itemName;

    public string ItemName { get { return _itemName; } }
}