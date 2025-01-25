using NUnit.Framework;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Collider))]
public class ItemDetector : MonoBehaviour
{
    [SerializeField] Collider _triggerCollider;
    [SerializeField] ItemEnums _requiredItemEnum;
    [SerializeField] UnityEvent _itemAction;

    private void Awake()
    {
        Assert.IsTrue(GetComponents<Collider>().Any(collider => collider.isTrigger = true), "No trigger collider attached");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Item Detector OnTrigger");
        Debug.Log($"Found player: {other.TryGetComponent<Player>(out Player _)} HasItem: {InventoryManager.Instance.HasItem(_requiredItemEnum)}");
        if (other.TryGetComponent<Player>(out Player _) && InventoryManager.Instance.HasItem(_requiredItemEnum))
        {
            Debug.Log("Found item?");
            _itemAction?.Invoke();
        }
    }
}