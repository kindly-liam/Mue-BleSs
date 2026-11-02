using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class Spawner : MonoBehaviour
{
    [SerializeField] protected int SpawnedEnemies = 0;
    [SerializeField] private List<GameObject> SpawnPoints = new List<GameObject>();
    [SerializeField] private List<GameObject> Enemys = new List<GameObject>();
    protected void SpawnNewEnemy()
    {
        int random = Random.Range(0, SpawnPoints.Count);
        int randomEnemy = Random.Range(0, Enemys.Count);
        SpawnedEnemies++;
        Instantiate(Enemys[randomEnemy], SpawnPoints[random].transform.position, transform.rotation);
    }
    void Update()
    {

    }
}
