using UnityEngine;

public class PowerCore : MonoBehaviour
{
    // Needs to track whether it's been picked up.
    // When picked up, we parent it to the character and we turn off phgrics gravity
    // When dropped, we restart physics and put it in front of the character, with no parent.

    [SerializeField] private bool _beingCarried = false;

    [SerializeField] private Rigidbody _rigidBody;

    public bool PickUp()
    {

    }
}