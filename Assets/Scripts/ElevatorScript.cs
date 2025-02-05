using Cinemachine;
using DG.Tweening;
using StarterAssets;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class ElevatorScript : MonoBehaviour
{
    [SerializeField] Transform[] _destinations;
    [SerializeField] int _position = 0;
    [SerializeField] bool _goingUp = true;
    [SerializeField] bool _inTransit = false;
    [SerializeField] float _elevatorSpeed = 3f;
    private float _transitTime;

    [SerializeField] CinemachineVirtualCamera _elevatorCam;

    [SerializeField] bool _isOperable = false;

    [SerializeField] PlayerInput _playerInput;
    [SerializeField] Canvas _elevatorUI;

    private void Awake()
    {
        _elevatorUI.enabled = false;
        _playerInput = GameObject.FindWithTag("Player").GetComponent<PlayerInput>();
    }

    public void SetOperable(bool isOperable)
    {
        _isOperable = isOperable;
    }

    public void ToggleElevator()
    {
        if (!_isOperable)   return;
        if (_inTransit)     return;

        if (_goingUp && _position == _destinations.Length - 1 || 
            !_goingUp && _position == 0) {
            _goingUp = !_goingUp;
        }

        if (_goingUp)
        {
            _position++;
        } else
        {
            _position--;
        }

        _inTransit = true;

        _transitTime = Vector3.Distance(this.transform.position, _destinations[_position].position) / _elevatorSpeed;

        _elevatorCam.Priority = 50;

        transform.DOMove(_destinations[_position].position, _transitTime).OnComplete(() => { _inTransit = false; _elevatorCam.Priority = 10; });
    }

    public void OpenElevatorUI()
    {
        if (!_isOperable) return;
        if (_inTransit) return;

        _elevatorUI.enabled = true;
        _playerInput.SwitchCurrentActionMap("UI");
    }

    public void SendToFloor(int floor)
    {
        if (!_isOperable) return;
        if (_inTransit) return;

        if (floor < 0 || floor >=  _destinations.Length) {
            Debug.LogError($"Attempted to send {gameObject.name} to invalid floor {floor}", this);
        }

        _inTransit = true;
        _elevatorCam.Priority = 50;
        _elevatorUI.enabled = false;
        _playerInput.SwitchCurrentActionMap("Player");

        _transitTime = Vector3.Distance(this.transform.position, _destinations[floor].position) / _elevatorSpeed;
        transform.DOMove(_destinations[floor].position, _transitTime).OnComplete(() => { 
            _inTransit = false; 
            _elevatorCam.Priority = 10;
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out _))
        {
            other.gameObject.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out _))
        {
            other.gameObject.transform.parent = null;
        }
    }
}