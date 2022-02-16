using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour

{
    [SerializeField] private Enemy _enemy;

    private Transform[] _spawnPoints;

    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        int i = 0;
        while (true)
        {
            Instantiate(_enemy);
            _enemy.transform.position = _spawnPoints[i].transform.position;
            i = (i + 1) / _spawnPoints.Length;
            new WaitForSeconds(2);
        }
    }
}
