using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Spawner : MonoBehaviour
{
    [Header("Configuración de Prefabs y Puntos")]
    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();
    [SerializeField] private List<GameObject> enemigos = new List<GameObject>();
    [SerializeField] private float intervaloSpawn = 3f;

   


    public static event Action OnWaveCompleted;

    private int enemigosTotalesOleada;
    private int enemigosSpawneados = 0;
    private Coroutine rutinaSpawn;

    public void IniciarOleada(int cantidadEnemigos)
    {
        enemigosTotalesOleada = cantidadEnemigos;
        enemigosSpawneados = 0;

        if (rutinaSpawn != null) StopCoroutine(rutinaSpawn);
        rutinaSpawn = StartCoroutine(RutinaSpawnear());
    }

    private IEnumerator RutinaSpawnear()
    {
        while (enemigosSpawneados < enemigosTotalesOleada)
        {
            yield return new WaitForSeconds(intervaloSpawn);

            if (spawnPoints.Count > 0 && enemigos.Count > 0)
            {
                int puntoRandom = UnityEngine.Random.Range(0, spawnPoints.Count);
                int enemigoRandom = UnityEngine.Random.Range(0, enemigos.Count);

                if (enemigos[enemigoRandom] != null && spawnPoints[puntoRandom] != null)
                {
                    

                    Instantiate(enemigos[enemigoRandom], spawnPoints[puntoRandom].transform.position, Quaternion.identity);


                    enemigosSpawneados++;

                    AudioSource audioPunto = spawnPoints[puntoRandom].GetComponent<AudioSource>();
                    if (audioPunto != null)
                    {
                        audioPunto.PlayOneShot(audioPunto.clip); 
                    }
                }
            }
        }
       

        


        OnWaveCompleted?.Invoke();
    }
}