using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ChestScript : MonoBehaviour
{
    Animator _animator;
    int _isOpenHash;
    [SerializeField] bool _isOperable = true;
    [SerializeField] string _cannotOperateMessage = "Can't do that, boss";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        Assert.IsNotNull(_animator);
        _isOpenHash = Animator.StringToHash("IsOpen");
    }

    public void OperateChest(bool isOpen)
    {
        if (_isOperable)
        {
            _animator.SetBool(_isOpenHash, isOpen);
        }
        else
        {
            Debug.Log(_cannotOperateMessage);
        }
    }

    public void ToggleChest()
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

    public void SetChestOperable(bool isOperable)
    {
        _isOperable = isOperable;
    }
}
