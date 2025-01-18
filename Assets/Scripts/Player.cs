using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int _points = 0;

    public void AdjustPoints(int points)
    {
        _points += points;
    }
}
