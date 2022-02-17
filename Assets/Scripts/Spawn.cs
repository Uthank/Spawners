using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour

{
    [SerializeField] private Enemy _enemy;

    private List<Transform> _spawnPoints = new List<Transform>();

    private void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
            _spawnPoints.Add(gameObject.transform.GetChild(i));

        Debug.Log(_spawnPoints.Count);
        StartCoroutine(SpawnFromPoints());
    }

    IEnumerator SpawnFromPoints()
    {
        Enemy enemy;

        while (true)
        {
            for (int i = 0; i < _spawnPoints.Count; i++)
            {
                enemy = Instantiate(_enemy);
                enemy.transform.position = _spawnPoints[i].position;
                yield return new WaitForSeconds(2f);
            }
        }
    }
}