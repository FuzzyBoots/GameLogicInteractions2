using NUnit.Framework;
using StarterAssets;
using UnityEngine;

public class InputInteractions : MonoBehaviour
{
    private StarterAssetsInputs _starterAssetsInputs;

    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        
        Assert.IsNotNull( _starterAssetsInputs, "No Starter Assets Inputs found on player!");
    }

    private void LateUpdate()
    {
        _starterAssetsInputs.interact = false;
        _starterAssetsInputs.cancel = false;
    }
}
