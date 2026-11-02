using UnityEngine;
using System.Collections.Generic;

public class Spawner2 : MonoBehaviour
{
    [SerializeField] private List<GameObject> SpawnPoints = new List<GameObject>();
    [SerializeField] private List<GameObject> Enemigos = new List<GameObject>();

    [SerializeField] private int maxEnemigos = 6;
    protected int spawnedEnemys = 0;

    void Start()
    {
        InvokeRepeating("spawnEnemyNew", 5, 5);
    }

    protected void spawnEnemyNew()
    {
        if (spawnedEnemys >= maxEnemigos)
        {
            CancelInvoke("spawnEnemyNew");
            return;
        }

        int puntoRandom = Random.Range(0, SpawnPoints.Count);
        int enemigoRandom = Random.Range(0, Enemigos.Count);

        Instantiate(Enemigos[enemigoRandom], SpawnPoints[puntoRandom].transform.position, transform.rotation);
        spawnedEnemys++;
    }
}