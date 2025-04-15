using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyAI : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    private int regenerationAmount = 20;

    private float regenerationDelay = 10f;

    private float regenerationRate = 1f;

    private int regenerationPerTick = 1;

    private float lastDamageTime;

    private Coroutine regenerationCoroutine;

    private int regenerationCap;

    public GameObject ally;
    public Transform respawnPoint;


    public Transform enemy;
    public float detectionRange = 15f;
    public float shootingRange = 7f;
    public float fireRate = 1f;
    public float bulletSpeed = 10f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private NavMeshAgent agent;
    private float nextFireTime;

    [Header("Audio")]
    public AudioSource bulletSound;
    public AudioClip shootSound;

    private void Start()
    {
        if (bulletSound == null)
        {
            bulletSound = GetComponent<AudioSource>();
        }

        currentHealth = maxHealth;

        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, enemy.position);
        if (distanceToPlayer <= detectionRange)
        {
            agent.SetDestination(enemy.position);
        }

        if (distanceToPlayer <= shootingRange)
        {
            agent.isStopped = true;
            SmoothLookAt(enemy);

            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
        else
        {
            agent.isStopped = false;
        }
        if (Time.time - lastDamageTime >= regenerationDelay && regenerationCoroutine == null)
        {
            regenerationCap = Mathf.Min(currentHealth + regenerationAmount, maxHealth);
            regenerationCoroutine = StartCoroutine(RegenerateHealth());
        }
        Die();
    }

    private void Shoot()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;

            if (bulletSound != null && shootSound != null)
            {
                bulletSound.PlayOneShot(shootSound);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (regenerationCoroutine != null)
        {
            StopCoroutine(regenerationCoroutine);
            regenerationCoroutine = null;
        }

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);
        lastDamageTime = Time.time;
    }

    private System.Collections.IEnumerator RegenerateHealth()
    {
        int targetHealth = Mathf.Min(currentHealth + regenerationAmount, maxHealth);

        while (currentHealth < targetHealth)
        {
            currentHealth += regenerationPerTick;
            currentHealth = Mathf.Min(currentHealth, regenerationCap);
            yield return new WaitForSeconds(regenerationRate);
        }

        regenerationCoroutine = null;
    }

    private void Die()
    {
        if (currentHealth <= 0)
        {
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        ally.transform.position = respawnPoint.position;

        yield return new WaitForSeconds(0.1f);

        currentHealth = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(20);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Void"))
        {
            TakeDamage(100);
        }
    }

    private void SmoothLookAt(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}
