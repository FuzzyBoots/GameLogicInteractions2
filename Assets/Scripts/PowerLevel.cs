using UnityEngine;

[CreateAssetMenu(menuName ="Power Levels")]
public class PowerLevel : ScriptableObject
{
    // Field to handle what power level is being used
    [SerializeField] int amount;

    // Method to apply the power level to the power object
    public void Apply(PowerCore powerCore)
    {
        powerCore.AdjustPowerLevel(amount);
    }    
}