using UnityEngine;

public class LootScript : MonoBehaviour
{
    public void SendToInventory(InventoryItem item)
    {
        InventoryManager.Instance.AddItem(item);

        // Get rid of the object in the world
        item.gameObject.SetActive(false);
    }
}
