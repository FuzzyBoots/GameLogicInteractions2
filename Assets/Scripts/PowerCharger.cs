using UnityEngine;

public class PowerCharger : PowerBase
{
    [SerializeField] PowerLevel _powerLevel;

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        // Do power transfer
        if (collision.gameObject.CompareTag("PowerCore"))
        {
            PowerCore powerCore = collision.gameObject.GetComponent<PowerCore>();

            _powerLevel.Apply(powerCore);
        }
    }
}