using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] GameObject _helpText;

    [SerializeField] UnityEvent _onInteract;

    [SerializeField] bool _inZone = false;

    [SerializeField] private void Update()
    {
        if (_inZone && Input.GetKeyDown(KeyCode.E))
        {
            _onInteract?.Invoke();
        }
    }

    private void Awake()
    {
        if (_helpText == null)
        {
            // Try to find it. We'll assume it has UILookAt
            _helpText = transform.GetComponentInChildren<UILookAt>()?.gameObject;
        }
        _helpText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player _))
        {
            _inZone = true;
            if (_helpText != null)
            {
                _helpText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player _))
        {
            _inZone = false;
            if (_helpText != null)
            {
                _helpText.SetActive(false);
            }
        }
    }
}
