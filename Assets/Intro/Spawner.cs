using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefabToSpawn = null;
    [SerializeField] [Range(0f, 10f)] private float _period = 0f;
    [SerializeField] [Range(2f, 100f)] private float _areaRadius = 0f;
    private Transform _transform;
    private float _timer;
    //private float _maxPrefabExtent;
    private Vector3 _randomPosition;

    private void Awake()
    {
        if (!_prefabToSpawn) Debug.LogWarning("Missing Prefab To Spawn reference in the inspector", this);
        _transform = transform;
        _timer = _period;
        //Collider prefabCollider = _prefabToSpawn.GetComponent<Collider>();
        //if (prefabCollider)
        //{
        //    Vector3 prefabExtents = prefabCollider.bounds.extents;
        //    _maxPrefabExtent = Mathf.Max(prefabExtents.x, prefabExtents.y, prefabExtents.z);
        //}
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _randomPosition = ComputeRandomPosition();
            GameObject spawnedObject = Instantiate(_prefabToSpawn, _randomPosition, Random.rotation);
            _timer = _period;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _areaRadius);
        //Gizmos.DrawWireSphere(_randomPosition, _maxPrefabExtent);
    }

    private Vector3 ComputeRandomPosition()
    {
        //Vector3 randomPosition;
        //do
        //{
        //    randomPosition = _transform.position + Random.insideUnitSphere * _areaRadius;
        //}
        //while (Physics.CheckSphere(randomPosition, _maxPrefabExtent));
        Vector3 randomPosition = _transform.position + Random.insideUnitSphere * _areaRadius;
        return randomPosition;
    }
}
