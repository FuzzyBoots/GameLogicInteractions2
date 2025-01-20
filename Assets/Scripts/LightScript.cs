using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    struct EmissionMat
    {
        public Material mat;
        public Color initialIntensity;
    }
    private List<EmissionMat> _emissionMaterials;

    private bool _lightOn = true;

    private void Awake()
    {
        _emissionMaterials = new List<EmissionMat>();
        // Try to find the emissive material
        MeshRenderer mr = GetComponent<MeshRenderer>();
        foreach (Material mat in mr.materials)
        {
            if (mat.IsKeywordEnabled("_EMISSION"))
            {
                EmissionMat matEntry;
                matEntry.mat = mat;
                matEntry.initialIntensity = mat.GetColor("_EmissionColor");
                _emissionMaterials.Add(matEntry);
            }
        }
    }

    public void ToggleLights()
    {
        _lightOn = !_lightOn;
        foreach (EmissionMat matEntry in _emissionMaterials)
        {
            matEntry.mat.SetColor("_EmissionColor", _lightOn ? matEntry.initialIntensity : Color.black);
        }
    }
}
