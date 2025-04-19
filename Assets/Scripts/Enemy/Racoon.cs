using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Racoon : MonoBehaviour
{
    public EnemyAIWave racoon;

    private float probability;
    private double distance;
    private float timer=0;
    private double speed;
    private float stealingTimer;
    private float startingShootingRange;
    private Vector3 originalPosition;
    //private Vector3 spawn = new Vector3(-34.86f, 1.47f, 18.58f);

    private bool actionExecuted = false;

    private void Start()
    {
        speed = racoon.agent.speed;

        startingShootingRange = racoon.shootingRange;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2 && !actionExecuted)
        {
            probability = Random.Range(0f,1f);
            if (probability >= 0.8f && racoon.playerStats.coins>=5)//Ahora mismo el mapache solo tiene en cuenta la posici�n inicial desde que decide perseguir al jugador
                                                                   //As� que ser�a conveniente que mejor persiga al jugador, le robe y luego escape
            {
                originalPosition = racoon.transform.position;
                distance = racoon.distanceToPlayer;
                stealingTimer = (float)(distance / speed);
                actionExecuted = true;
                racoon.shootingRange = 2f;

                racoon.canShoot = false;
                
                StartCoroutine(Stealing());
                
                timer = 0;
            }
            else
            {
                timer = 0;
            }
            //Quiz�, haga falta a�adir aqu� un actionExecuted = false;
        }
    }

    private IEnumerator Stealing()
    {
        //A�adir aqu� una l�nea de c�digo que haga m�s smooth el giro del enemigo a su posicion original
        yield return new WaitForSeconds(stealingTimer); //Este valor deber� ser cambiado por una variable que calcule el tiempo que tardar�a el mapache en volver a su posici�n original 
        racoon.playerStats.coins -= Random.Range(1, 6);
        Debug.Log("You've been robbed!");
        racoon.agent.ResetPath();
        racoon.agent.SetDestination(originalPosition);
        yield return new WaitForSeconds(stealingTimer);
        racoon.shootingRange = startingShootingRange;
        racoon.canShoot = true;
        actionExecuted = false;
    }
}
