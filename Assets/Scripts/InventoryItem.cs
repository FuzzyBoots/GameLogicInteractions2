using UnityEngine;

public enum ItemEnums
{
    Unspecified = 0,
    KeyCardA
}

public class InventoryItem : MonoBehaviour
{
    [SerializeField] string _itemName;
    [SerializeField] ItemEnums _itemEnum;

    public string ItemName { get { return _itemName; } }

    public ItemEnums ItemEnum { get { return _itemEnum; }}
}