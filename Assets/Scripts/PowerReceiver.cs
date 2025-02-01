using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

class PowerReceiver : PowerBase
{
    [SerializeField] int _requiredLevel;
    [SerializeField] UnityEvent _onPoweredUp;
    [SerializeField] UnityEvent _onCellRemoval;

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        // Do power transfer
        if (collision.gameObject.CompareTag("PowerCore"))
        {
            PowerCore powerCore = collision.gameObject.GetComponent<PowerCore>();

            if (powerCore.PowerLevel == _requiredLevel)
            {
                _onPoweredUp.Invoke();
            }
        }
    }

    protected override void OnCollisionExit(Collision collision)
    {
        base.OnCollisionExit(collision);

        if (collision.gameObject.CompareTag("PowerCore"))
        {
            // Run the event for power core removal
            _onCellRemoval.Invoke();
        }
    }
}

