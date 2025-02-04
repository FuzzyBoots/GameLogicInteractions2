using NUnit.Framework;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    StarterAssetsInputs _starterAssetsInputs;

    [SerializeField] GameObject _helpText;

    [SerializeField] UnityEvent _onInteract;

    bool _inZone = false;

    [SerializeField] private void Update()
    {
        if (_inZone && _starterAssetsInputs.interact)
        {
            _onInteract?.Invoke();
        }
    }

    private void Awake()
    {
        _starterAssetsInputs = GameObject.FindWithTag("Player").GetComponent<StarterAssetsInputs>();

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
