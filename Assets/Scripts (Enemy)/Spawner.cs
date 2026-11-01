using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> SpawnPoints = new List<GameObject>();
    [SerializeField] private List<GameObject> Enemigos = new List<GameObject>();

    [SerializeField] private int maxEnemigos = 6;
    private int enemigosSpawneados = 0;

    void Start()
    {
        InvokeRepeating("SpawnNewEnemy", 5, 5);
    }

    void SpawnNewEnemy()
    {
        if (enemigosSpawneados >= maxEnemigos)
        {
            CancelInvoke("SpawnNewEnemy");
            return;
        }

        int puntoRandom = Random.Range(0, SpawnPoints.Count);
        int enemigoRandom = Random.Range(0, Enemigos.Count);

        Instantiate(Enemigos[enemigoRandom], SpawnPoints[puntoRandom].transform.position, transform.rotation);
        enemigosSpawneados++;
    }
}