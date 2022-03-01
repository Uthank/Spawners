using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private List<Transform> _positions = new List<Transform>();
    private WaitForSeconds _delay = new WaitForSeconds(2f);

    private void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
            _positions.Add(gameObject.transform.GetChild(i));

        StartCoroutine(SpawnFromPoints());
    }

    private IEnumerator SpawnFromPoints()
    {
        Enemy enemy;

        while (true)
        {
            for (int i = 0; i < _positions.Count; i++)
            {
                enemy = Instantiate(_enemy);
                enemy.transform.position = _positions[i].position;
                yield return _delay;
            }
        }
    }
}