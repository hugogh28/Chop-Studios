using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamStats : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;
    public GameObject dam;

    void Start()
    {
        dam.SetActive(true);
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Die();
    }

    private void Die()
    {
        if(currentHealth <= 0)
        {
            dam.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
            TakeDamage(20);
    }

    private void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);
    }
}
