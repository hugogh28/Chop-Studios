using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;

    private void Awake()
    {
        Destroy(gameObject, life);
    }
    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.CompareTag("Destroyable")))
        {
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }
}
