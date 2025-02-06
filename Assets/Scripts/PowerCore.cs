using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PowerCore : MonoBehaviour
{
    // Needs to track whether it's been picked up.
    // When picked up, we parent it to the character and we turn off phgrics gravity
    // When dropped, we restart physics and put it in front of the character, with no parent.

    [SerializeField] private bool _beingCarried = false;

    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Transform _carryTransform;

    [SerializeField] int _charge = 0;

    [SerializeField] int _fullCharge = 10;

    [SerializeField] private List<Material> _emissionMaterials;

    Animator _playerAnimator;

    [SerializeField] GameObject _emissionObject;

    [SerializeField] float _chargeTime = 5f;

    public int PowerLevel { get { return _charge; } internal set { _charge = value; } }

    private void Awake()
    {
        _beingCarried = false;

        _rigidBody = GetComponent<Rigidbody>();

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
        SetPowerCoreColor();
    }

    public void PickUpOrDrop()
    {
        if (!_beingCarried)
        {
            _beingCarried = true;
            _rigidBody.isKinematic = true;

            transform.position = _carryTransform.position;
            transform.rotation = _carryTransform.rotation;
            transform.parent = _carryTransform;

            if (_playerAnimator != null)
            {
                _playerAnimator.SetBool("Carrying", true);
            }
        } else
        {
            _beingCarried = false;
            _rigidBody.isKinematic = false;

            transform.parent = null;

            if (_playerAnimator != null)
            {
                _playerAnimator.SetBool("Carrying", false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.gameObject.TryGetComponent<Animator>(out _playerAnimator);
        }
    }

    public void SetPlayerCarryPos(Transform position)
    {
        _carryTransform = position;
    }

    internal void AdjustPowerLevel(int amount)
    {
        _charge += amount;
        SetPowerCoreColor();
    }

    private void SetPowerCoreColor()
    {
        Color coreColor;
        
        float chargePercentage = _charge / (float)_fullCharge;

        if (chargePercentage > 1.01f)
        {
            coreColor = Color.red;
        } else if (chargePercentage >= 0.99f)
        {
            coreColor = Color.green;
        } else if (chargePercentage >= 0.6f)
        {
            coreColor = Color.cyan;
        } else if (chargePercentage >= 0.3f)
        {
            coreColor = Color.blue;
        } else
        {
            coreColor = Color.magenta;
        }

        foreach (Material mat in _emissionMaterials)
        {
            mat.DOColor(coreColor, "_EmissionColor", _chargeTime);
        }
    }
}