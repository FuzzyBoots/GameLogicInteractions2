using NUnit.Framework;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] GameObject _helpText;

    private void OnTriggerEnter(Collider other)
    {
        if (_helpText != null && other.TryGetComponent<Player>(out Player _))
        {
            _helpText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_helpText != null && other.TryGetComponent<Player>(out Player _))
        {
            _helpText.SetActive(false);
        }
    }
}
