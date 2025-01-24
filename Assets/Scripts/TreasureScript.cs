using System.Collections;
using UnityEngine;

public class TreasureScript : MonoBehaviour
{
    [SerializeField] GameObject[] _treasureList;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] [Range(0.1f, 5f)] float _spawnInterval;

    private WaitForSeconds _spawnTimer;

    private bool _hasDispensed = false;

    private void Awake()
    {
        _spawnTimer = new WaitForSeconds(_spawnInterval);
    }

    public void SpawnTreasure()
    {
        if (_hasDispensed) return;

        _hasDispensed = true;

        StartCoroutine(SequentiallySpawn());        
    }

    private IEnumerator SequentiallySpawn()
    {
        foreach (GameObject treasure in _treasureList)
        {
            Instantiate(treasure, _spawnPoint);
            yield return _spawnTimer;
        }
    }
}
