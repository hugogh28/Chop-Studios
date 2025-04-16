using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;

    private WaveSpawner waveSpawner;

    private float countdown = 5f;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime);

        countdown -= Time.deltaTime;

        if(countdown <= 0)
        {
            Destroy(gameObject);

            waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesLeft--;
        }
    }
    public void SetWaveSpawner(WaveSpawner spawner)
    {
        waveSpawner = spawner;
    }
}
