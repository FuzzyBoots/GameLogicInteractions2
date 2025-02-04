using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On elevator");
        if (other.gameObject.TryGetComponent<Player>(out _))
        {
            Debug.Log("Parented to platform");
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