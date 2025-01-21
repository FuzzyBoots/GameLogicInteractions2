using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent _onDepressed;
    [SerializeField] UnityEvent _onReleased;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Depressed by {collision.gameObject.name}");
        _onDepressed?.Invoke();
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log($"Released by {collision.gameObject.name}");
        _onReleased?.Invoke();
    }
}
