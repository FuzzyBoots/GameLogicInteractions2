using NUnit.Framework;
using UnityEngine;

[RequireComponent (typeof(Animator))]
public class DoorScript : MonoBehaviour
{
    Animator _animator;
    int _isOpenHash;
    [SerializeField] bool _isOperable = true;
    [SerializeField] string _cannotOperateMessage = "Can't do that, boss";

    private void Awake()
    {
        _animator = GetComponent<Animator> ();
        Assert.IsNotNull(_animator);
        _isOpenHash = Animator.StringToHash("IsOpen");
    }

    public void OperateDoor(bool isOpen)
    {
        if (_isOperable)
        {
            _animator.SetBool(_isOpenHash, isOpen);
        } else
        {
            Debug.Log(_cannotOperateMessage);
        }
    }

    public void ToggleDoor()
    {
        if (_isOperable)
        {
            bool isOpen = _animator.GetBool(_isOpenHash);
            _animator.SetBool(_isOpenHash, !isOpen);
        }
        else
        {
            Debug.Log(_cannotOperateMessage);
        }
    }

    public void SetDoorOperable(bool isOperable)
    {
        _isOperable = isOperable;
    }
}
