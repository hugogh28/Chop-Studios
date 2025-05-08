using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float countdown;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private float spawnRadius = 3f;

    public Wave[] waves;

    public int currentWaveIndex = 0;

    private bool readyToCountDown;

    private void Start()
    {
        readyToCountDown = true;

        for(int i=0; i<waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].enemies.Length;
        }
    }

    private void Update()
    {
        if(currentWaveIndex >= waves.Length)
        {
            SceneManager.LoadScene(1);
            Debug.Log("You survived every wave!");
            return;
        }

        if(readyToCountDown)
        {
            countdown -= Time.deltaTime;
        }

        if(countdown <= 0)
        {
            readyToCountDown = false;

            countdown = waves[currentWaveIndex].timeToNextWave;

            StartCoroutine(SpawnWave());
        }

        if (waves[currentWaveIndex].enemiesLeft == 0)
        {

            readyToCountDown = true;
            currentWaveIndex++;
        }
    }

    private IEnumerator SpawnWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++) { 

                Vector3 randomOffSet = UnityEngine.Random.insideUnitSphere;

                randomOffSet.y = 0f;

                randomOffSet *= spawnRadius;

                Vector3 spawnPosition = spawnPoint.transform.position + randomOffSet;

                //Enemy enemy = Instantiate(waves[currentWaveIndex].enemies[i], spawnPoint.transform);

                EnemyAIWave enemy = Instantiate(waves[currentWaveIndex].enemies[i], spawnPosition, Quaternion.identity);

                //enemy.transform.SetParent(spawnPoint.transform);

                enemy.SetWaveSpawner(this);

                GameObject playerObj = GameObject.FindWithTag("Player");

                if (playerObj != null)
                {
                    enemy.player = playerObj.transform;

                    PlayerStats playerStats = playerObj.GetComponent<PlayerStats>();
                    if(playerStats != null)
                    {
                        enemy.playerStats = playerStats;
                    }
                    else
                    {
                        Debug.LogWarning("No se ha encontrado el componente PlayerStats en el jugador");
                    }
                }
                else
                {
                    Debug.LogWarning("No se ha encontrado un jugador");
                }
                
                yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(spawnPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(spawnPoint.transform.position, spawnRadius);
        }
    }
}

[System.Serializable]

public class Wave
{
    public EnemyAIWave[] enemies;
    public float timeToNextEnemy;
    public float timeToNextWave;

    [HideInInspector] public int enemiesLeft;
}
