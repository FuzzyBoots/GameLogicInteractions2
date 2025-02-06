using Cinemachine;
using DG.Tweening;
using StarterAssets;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class ElevatorScript : MonoBehaviour, ICancelHandler
{
    [SerializeField] Transform[] _destinations;
    [SerializeField] int _position = 0;
    [SerializeField] bool _goingUp = true;
    [SerializeField] bool _inTransit = false;
    [SerializeField] float _elevatorSpeed = 3f;
    private float _transitTime;

    [SerializeField] CinemachineVirtualCamera _elevatorCam;

    [SerializeField] bool _isOperable = false;

    InputAction _cancel;

    [SerializeField] PlayerInput _playerInput;
    [SerializeField] Canvas _elevatorUI;

    private void Update()
    {
        if (_cancel.IsPressed())
        {
            CancelUI();
        }
    }

    private void Awake()
    {
        _elevatorUI.enabled = false;
        _playerInput = GameObject.FindWithTag("Player").GetComponent<PlayerInput>();

        _cancel = InputSystem.actions.FindAction("Cancel");
    }

    public void SetOperable(bool isOperable)
    {
        Debug.Log("Operable");
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
        Debug.Log($"Operable: {_isOperable} In Transit: {_inTransit}", this);
        if (!_isOperable) return;
        Debug.Log("Past Operable");
        if (_inTransit) return;
        Debug.Log("Past Transit");
        Debug.Log("OpenElevatorUI");
        StartCoroutine(SwitchToUIAfterAFrame());
    }

    public IEnumerator SwitchToUIAfterAFrame()
    {
        Debug.Log("Before wait");
        yield return new WaitForSeconds(0.1f);
        Debug.Log("After wait");
        _elevatorUI.enabled = true;
        _playerInput.SwitchCurrentActionMap("UI");
    }

    public void CancelUI()
    {
        _elevatorUI.enabled = false;
        _playerInput.SwitchCurrentActionMap("Player");
    }

    public void SendToFloor(int floor)
    {
        Debug.Log($"Send to Floor {floor}", this);
        if (!_isOperable) return;
        if (_inTransit) return;

        if (floor < 0 || floor >=  _destinations.Length) {
            Debug.LogError($"Attempted to send {gameObject.name} to invalid floor {floor}", this);
        }

        _inTransit = true;
        _elevatorCam.Priority = 50;
        
        CancelUI();

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

    public void OnCancel(BaseEventData eventData)
    {
        Debug.Log("Cancel?");
    }
}