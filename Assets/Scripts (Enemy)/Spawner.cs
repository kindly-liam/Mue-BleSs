using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class Spawner : MonoBehaviour
{
    [SerializeField] protected int SpawnedEnemies = 0;
    [SerializeField] private List<GameObject> SpawnPoints = new List<GameObject>();
    [SerializeField] private GameObject Enemy;

    //void Start()
    //{
    //    InvokeRepeating("SpawnNewEnemy", 5, 5);
    //}
    protected void SpawnNewEnemy()
    {
        int random = Random.Range(0, SpawnPoints.Count);
        SpawnedEnemies++;
        Instantiate(Enemy, SpawnPoints[random].transform.position, transform.rotation);
    }
    void Update()
    {

    }
}
