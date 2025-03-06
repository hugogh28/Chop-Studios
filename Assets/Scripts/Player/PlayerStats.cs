using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    private int currentHealth;

    public HealthBar healthBar;

    private int regenerationAmount = 20;

    private float regenerationDelay = 10f;

    private float regenerationRate = 1f;

    private int regenerationPerTick = 1;

    private float lastDamageTime;
    private Coroutine regenerationCoroutine;
    private int regenerationCap;

    public GameObject player;
    public Transform respawnPoint;

    private void Start()
    {
        currentHealth = maxHealth;

        healthBar.SetSliderMax(maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if(regenerationCoroutine != null)
        {
            StopCoroutine(regenerationCoroutine);
            regenerationCoroutine = null;
        }

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);
        healthBar.SetSlider(currentHealth);
        lastDamageTime = Time.time;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.K)) {
            TakeDamage(20);
        }
        if(Time.time - lastDamageTime >= regenerationDelay && regenerationCoroutine == null)
        {
            regenerationCap = Mathf.Min(currentHealth + regenerationAmount, maxHealth);
            regenerationCoroutine = StartCoroutine(RegenerateHealth());
        }
        Die();
    }

    private System.Collections.IEnumerator RegenerateHealth()
    {
        int targetHealth = Mathf.Min(currentHealth + regenerationAmount, maxHealth);

        while (currentHealth < targetHealth)
        {
            currentHealth += regenerationPerTick;
            currentHealth = Mathf.Min(currentHealth, regenerationCap);
            healthBar.SetSlider(currentHealth);
            yield return new WaitForSeconds(regenerationRate);
        }

        regenerationCoroutine = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(20);
        }
    }

    private IEnumerator Respawn()
    {
        player.transform.position = respawnPoint.position;

        yield return new WaitForSeconds(0.1f);

        currentHealth = maxHealth;

        healthBar.SetSlider(currentHealth);
    }

    private void Die()
    {
        if(currentHealth <= 0)
        {
            StartCoroutine(Respawn());
        }
    }
}
