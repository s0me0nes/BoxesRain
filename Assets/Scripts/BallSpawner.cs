using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private List<Ball> _balls = new List<Ball>();

    [SerializeField] private float _spawnTime;

    [SerializeField] private Vector3 _firstSpawnPosition;
    [SerializeField] private Vector3 _secondSpawnPosition;

    private void Awake()
    {
        if (_spawnTime <= 0)
        {
            _spawnTime = 1;
        }
    }

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        var timeToSpawn = new WaitForSeconds(1);

        while (true)
        {
            PoolObjects();

            yield return timeToSpawn;
        }
    }

    private void PoolObjects()
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();

        foreach (var obj in _balls)
        {
            if (!obj.gameObject.activeSelf)
            {
                obj.transform.position = randomSpawnPosition;
                obj.gameObject.SetActive(true);
                return;
            }
        }
        
        Ball newBall = Instantiate(_ballPrefab, randomSpawnPosition, Quaternion.identity);
        _balls.Add(newBall);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(
    Random.Range(_firstSpawnPosition.x, _secondSpawnPosition.x),
    Random.Range(_firstSpawnPosition.y, _secondSpawnPosition.y),
    Random.Range(_firstSpawnPosition.z, _secondSpawnPosition.z));
    }
}
