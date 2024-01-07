using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public List<GameObject> enemyPrefabs;
    public float spawnInterval;
}
public class WaveSpawnLogic : MonoBehaviour
{
    public List<Wave> waves;
    private int currentWaveIndex = 0;

    void Start()
    {
        StartNextWave();
    }

    void StartNextWave()
    {
        if (currentWaveIndex < waves.Count)
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            currentWaveIndex++;
        }
        else
        {
            Debug.Log("All waves completed!");
            // Game over logic, level complete, etc.
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        foreach (var enemyPrefab in wave.enemyPrefabs)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            NPC_Brain enemyScript = newEnemy.GetComponent<NPC_Brain>();
            enemyScript.OnEnemyDeath += OnEnemyDeath;

            yield return new WaitForSeconds(wave.spawnInterval);
        }
    }

    void OnEnemyDeath()
    {
        // Check if all enemies in the current wave have died
        if (AllEnemiesDead())
        {
            // Start the next wave
            StartNextWave();
        }
    }

    bool AllEnemiesDead()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        return enemies.Length == 0;
    }
}
