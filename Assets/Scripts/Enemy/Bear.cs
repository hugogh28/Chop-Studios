using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Transactions;
using UnityEngine;

public class Bear : MonoBehaviour
{
    public EnemyAIWave bear;
    public PlayerStats stats;
    private float timer = 0;
    private bool attacked = false;
    //private float probability;
    //private bool actionExecuted = false;

    private void Start()
    {
        bear.isAttacking = true;

        if(stats == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if(player != null)
            {
                stats = player.GetComponent<PlayerStats>();
            }
        }
    }

    private void Update()
    {
        bear.distanceToPlayer = Vector3.Distance(bear.transform.position, bear.player.position);
        if (bear.distanceToPlayer <= bear.shootingRange&&attacked==false)
        {
            stats.TakeDamage(50);
            attacked = true;
            StartCoroutine(Attacking(2));
        }
    }

    private IEnumerator Attacking(int amount)
    {
        yield return new WaitForSeconds(amount);
        attacked = false;
    }
}
