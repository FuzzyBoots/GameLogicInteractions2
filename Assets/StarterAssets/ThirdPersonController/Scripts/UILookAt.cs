using UnityEngine;

public class UILookAt : MonoBehaviour
{
    [SerializeField] Transform _cameraTransform;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(_cameraTransform);
    }
}
