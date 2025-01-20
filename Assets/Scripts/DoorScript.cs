using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Animator))]
public class DoorScript : MonoBehaviour
{
    Animator _animator;
    int _isOpenHash;
    [SerializeField] bool _isOperable = true;
    [SerializeField] string _cannotOperateMessage = "Can't do that, boss";

    [SerializeField] [ColorUsage(true, true)] Color _operableColor = Color.green * 2;
    [SerializeField] [ColorUsage(true, true)] Color _inOperableColor = Color.red * 2;

    [SerializeField] private List<Material> _emissionMaterials;

    [SerializeField] GameObject _emissionObject;

    private void Awake()
    {
        _animator = GetComponent<Animator> ();
        Assert.IsNotNull(_animator);
        _isOpenHash = Animator.StringToHash("IsOpen");

        if (_emissionObject && _emissionObject.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
        {
            foreach (Material mat in meshRenderer.materials)
            {
                if (mat.IsKeywordEnabled("_EMISSION"))
                {
                    _emissionMaterials.Add(mat);
                }
            }
        }

        UpdateEmissionColor();
    }

    private void UpdateEmissionColor()
    {
        Debug.Log("Should be changing the emission colors...");
        foreach (Material mat in _emissionMaterials)
        {
            Color newColor = _isOperable ? _operableColor : _inOperableColor;
            Debug.Log($"Setting {mat.name} to {newColor}");
            mat.SetColor("_EmissionColor", _isOperable ? _operableColor : _inOperableColor);
        }
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

        UpdateEmissionColor();
    }
}
