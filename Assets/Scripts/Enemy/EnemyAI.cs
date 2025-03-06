using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public Transform player;
    public float detectionRange = 15f;
    public float shootingRange = 7f;
    public float fireRate = 1.5f;
    public float bulletSpeed = 10;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private NavMeshAgent agent;
    private float nextFireTime;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if(distanceToPlayer <= detectionRange)
        {
            agent.SetDestination(player.position);
        }

        if(distanceToPlayer <= shootingRange)
        {
            agent.isStopped = true;
            transform.LookAt(player);

            if(Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
        else
        {
            agent.isStopped = false;
        }
    }

    private void Shoot()
    {
        if(bulletPrefab != null && bulletSpawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }
}
