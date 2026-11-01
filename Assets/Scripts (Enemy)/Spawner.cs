using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> SpawnPoints = new List<GameObject>();
    [SerializeField] private GameObject Enemy;

    [SerializeField] private int maxEnemigos = 3;   // nuevo: tope de enemigos
    private int enemigosSpawneados = 0;             // nuevo: contador

    void Start()
    {
        InvokeRepeating("SpawnNewEnemy", 5, 5);
    }

    void SpawnNewEnemy()
    {
        if (enemigosSpawneados >= maxEnemigos)   // nuevo: si ya llegó al tope
        {
            CancelInvoke("SpawnNewEnemy");       // detiene el spawn
            return;
        }

        int random = Random.Range(0, SpawnPoints.Count);
        Instantiate(Enemy, SpawnPoints[random].transform.position, transform.rotation);
        enemigosSpawneados++;                    // nuevo: suma al contador
    }
}