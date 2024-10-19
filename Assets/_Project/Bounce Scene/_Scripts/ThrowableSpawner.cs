using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _throwablePrefabs;
    [SerializeField] int spawnCount = 20;
    [SerializeField] float spawnRadius = 3f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            var shape = Instantiate(_throwablePrefabs[Random.Range(0, _throwablePrefabs.Length)], transform);
            shape.transform.SetPositionAndRotation(transform.position + Random.insideUnitSphere * spawnRadius, Quaternion.identity);
        }
    }
}
