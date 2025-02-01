using UnityEngine;

public class PowerBase: MonoBehaviour
{
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PowerCore"))
        {
            // Handle core insertion
            GameObject core = collision.gameObject;
            core.transform.position = transform.position;
            core.transform.rotation = transform.rotation;

            Debug.Log("Power core inserted");
        }
    }

    protected virtual void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("PowerCore"))
        {
            Debug.Log("Power core removed");
        }
    }
}