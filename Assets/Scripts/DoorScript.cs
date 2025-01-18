using NUnit.Framework;
using UnityEngine;

[RequireComponent (typeof(Animator))]
public class DoorScript : MonoBehaviour
{
    Animator _animator;
    int _isOpenHash;

    private void Awake()
    {
        _animator = GetComponent<Animator> ();
        Assert.IsNotNull(_animator);
        _isOpenHash = Animator.StringToHash("IsOpen");
    }

    public void OpenDoor()
    {
        _animator.SetBool(_isOpenHash, true);
    }

    public void CloseDoor()
    {
        _animator.SetBool(_isOpenHash, false);
    }

    public void ToggleDoor()
    {
        bool isOpen = _animator.GetBool(_isOpenHash);
        _animator.SetBool(_isOpenHash, !isOpen);
    }
}
