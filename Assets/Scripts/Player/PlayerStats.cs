using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;

    public int maxArmor = 50;

    public ShopUI shop;

    [Header("Shop Related")]

    public int armor = 0;

    public int coins = 100;

    public int currentHealth;

    public int currentArmor;

    public HealthBar healthBar;

   public HealthBar armorBar;

    private int regenerationAmount = 20;

    private float regenerationDelay = 10f;

    private float regenerationRate = 1f;

    private int regenerationPerTick = 1;

    private float lastDamageTime;
    private Coroutine regenerationCoroutine;
    private int regenerationCap;

    public GameObject player;
    public GameObject armorSlider;
    public Transform respawnPoint;

    private void Start()
    {
        currentArmor = 0;

        currentHealth = maxHealth;

        armorSlider.SetActive(false);

        healthBar.SetSliderMax(maxHealth);
        armorBar.SetSliderMax(maxArmor);

        armorBar.SetSlider(currentArmor);
    }

    public void AddHealth(int amount)
    {
        if (currentHealth + amount > 100)
        {
            currentHealth = maxHealth;
            healthBar.SetSliderMax(maxHealth);
        }
        else if(currentHealth + amount <=100)
        {
            currentHealth += amount;
            healthBar.SetSlider(currentHealth);
        }
    }

    public void AddArmor(int amount)
    {
        if (currentArmor + amount > 50)
        {
            currentArmor = maxArmor;
            armorBar.SetSliderMax(maxArmor);
        }
        else if (currentArmor + amount <= 50)
        {
            currentArmor += amount;
            armorBar.SetSlider(currentArmor);
        }
        
    }

    public bool SpendCoins(int amount)
    {
        if(coins >= amount)
        {
            coins -= amount;
            return true; 
        }
        return false;
    }

    public void TakeDamage(int amount)
    {
        int damage;
        if (regenerationCoroutine != null)
        {
            StopCoroutine(regenerationCoroutine);
            regenerationCoroutine = null;
        }
        if (currentArmor >= 1)
        {
            currentArmor -= amount;
            if (currentArmor < 1)
            {
                damage = -currentArmor;
                currentHealth -= damage;
                currentArmor = Mathf.Max(currentArmor, 0);
                currentHealth = Mathf.Max(currentHealth, 0);
                armorBar.SetSlider(currentArmor);
                healthBar.SetSlider(currentHealth);
                lastDamageTime = Time.time;
            }
            else
            {
                currentArmor = Mathf.Max(currentArmor, 0);
                armorBar.SetSlider(currentArmor);
                lastDamageTime = Time.time;
            }
        }
        else
        {
            currentHealth -= amount;
            currentHealth = Mathf.Max(currentHealth, 0);
            healthBar.SetSlider(currentHealth);
            lastDamageTime = Time.time;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(20);
        }
        if(currentArmor >= 1)
        {
            armorSlider.SetActive(true);
        }else if (currentArmor < 1)
        {
            armorSlider.SetActive(false);
        }
        if (Time.time - lastDamageTime >= regenerationDelay && regenerationCoroutine == null)
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
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(10);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Void"))
        {
            TakeDamage(200);//Comprobar si da errores este valor al haber añadido un escudo
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
        if(currentHealth <= 0)//Código por modificar, el personaje debe morir y no respawnear si lo matan los enemigos
        {
            StartCoroutine(Respawn());
        }
    }
}
