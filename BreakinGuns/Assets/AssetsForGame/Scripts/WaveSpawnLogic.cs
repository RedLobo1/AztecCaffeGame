using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnLogic : MonoBehaviour
{
    public List<Wave> waves;
    private int currentWaveIndex = 0;
    private InputMaster inputMaster;
    [SerializeField] private  GameObject _healthDropPrefab;
    [SerializeField] private  Transform _healthDropLocation;

    //enable input
    private void OnEnable()
    {
        inputMaster.Enable();
    }
    //on disable
    private void OnDisable()
    {
        inputMaster.Disable();
    }
    void Awake()
    {
        inputMaster = new InputMaster();
        StartNextWave();
    }


    void StartNextWave()
    {
        if (currentWaveIndex < waves.Count)
        {
            Instantiate(_healthDropPrefab, _healthDropLocation.position, Quaternion.identity);
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
            Transform childTransform = newEnemy.transform.Find("Enemy body");
            NPC_Brain enemyScript = new NPC_Brain();

            if (childTransform == null)
            {
                enemyScript = newEnemy.GetComponent<NPC_Brain>();
            }
            else
            {
                enemyScript = childTransform.GetComponent<NPC_Brain>();
            }


            if (enemyScript != null)
            {
                enemyScript.OnEnemyDeath += OnEnemyDeath;
            }
            
            else
            {
                Debug.LogError("NPC_Brain component not found on enemyPrefab.");
            }

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
