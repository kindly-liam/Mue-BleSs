using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class SistemadeOleadas : Spawner2
{
    protected float timer;
    [SerializeField] private int CurrentRound = 1;
    [SerializeField] private int RemainingEnemies;
    [SerializeField] private int EnemiesPerRound = 3;
    void Update()
    {
        Debug.Log("Ronda Actual: " + CurrentRound);
        if (spawnedEnemys < EnemiesPerRound)
        {
            if (timer > 5)
            {
                timer = 0;
                spawnEnemyNew();
            }
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        RemainingEnemies = enemies.Length;

        if (RemainingEnemies <= 0 && spawnedEnemys == EnemiesPerRound)
        {
            CurrentRound++;
            EnemiesPerRound = EnemiesPerRound + 2;
            spawnedEnemys = 0;
        }
        timer += Time.deltaTime;
    }
}
