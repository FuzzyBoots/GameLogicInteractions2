using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent _onDepressed;

    private void OnCollisionEnter(Collision collision)
    {
        _onDepressed?.Invoke();
    }
}
