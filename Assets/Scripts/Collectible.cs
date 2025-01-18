using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour
{
    [SerializeField] UnityEvent _onPickup;

    private void OnTriggerEnter(Collider other)
    {
        _onPickup?.Invoke();
        Destroy(gameObject);
    }
}
