using UnityEngine;

public class UILookAt : MonoBehaviour
{
    [SerializeField] Transform _cameraTransform;

    private void LateUpdate()
    {
        transform.LookAt(_cameraTransform);
    }
}
