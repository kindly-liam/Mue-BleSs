using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> SpawnPoints = new List<GameObject>();
    [SerializeField] private GameObject Enemy;

    void Start()
    {
        InvokeRepeating("SpawnNewEnemy", 5, 5);
    }
    void SpawnNewEnemy()
    {
        int random = Random.Range(0, SpawnPoints.Count);
        Instantiate(Enemy, SpawnPoints[random].transform.position, transform.rotation);
    }
}
