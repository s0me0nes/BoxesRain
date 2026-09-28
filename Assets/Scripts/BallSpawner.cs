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
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var timeToSpawn = new WaitForSeconds(1);

        while (true)
        {
            SpawnBall();

            yield return timeToSpawn;
        }
    }

    private void SpawnBall()
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

        Ball newObject = Instantiate(_ballPrefab, randomSpawnPosition, Quaternion.identity);

        if (newObject.TryGetComponent<Ball>(out var newBall))
        {
            _balls.Add(newBall);
        }
        else
        {
            Debug.Log("На префабе отсутствует компонент - " + _ballPrefab);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(
    Random.Range(_firstSpawnPosition.x, _secondSpawnPosition.x),
    Random.Range(_firstSpawnPosition.y, _secondSpawnPosition.y),
    Random.Range(_firstSpawnPosition.z, _secondSpawnPosition.z));
    }
}