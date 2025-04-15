using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10;
    public GameObject bulletPrefab;
    public float fireRate = 1f;

    private float nextFireTime = 0f;

    private bool canShoot = false;

    [Header("Audio")]
    public AudioSource bulletSound;
    public AudioClip shootSound;

    private void Start()
    {
        if(bulletSound == null)
        {
            bulletSound = GetComponent<AudioSource>();
        }

        Invoke(nameof(EnableShooting), 0.2f);
    }

    private void Update()
    {
        if (canShoot && Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        if(bulletPrefab != null && bulletSpawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;

            if(bulletSound != null && shootSound != null)
            {
                bulletSound.PlayOneShot(shootSound);
            }
        }
    }

    private void EnableShooting()
    {
        canShoot = true;
    }
}
