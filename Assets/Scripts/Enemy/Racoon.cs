using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Racoon : MonoBehaviour
{
    public EnemyAIWave racoon;

    private float probability;
    private float timer=0;
    private float startingShootingRange;
    private float startingDetectionRange;

    private bool actionExecuted = false;

    private Vector3 originalPosition;

    private void Start()
    {
        startingShootingRange = racoon.shootingRange;

        startingDetectionRange = racoon.detectionRange;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2 && !actionExecuted)
        {
            probability = Random.Range(0f,1f);
            if (probability >= 0.8f)
            {
                racoon.isStealing = true;
                actionExecuted = true;
                originalPosition = racoon.transform.position;
                racoon.canShoot = false;
                racoon.shootingRange = 4f;
                
                Debug.Log("No se ejecuta el if");
                if(racoon.distanceToPlayer <= racoon.shootingRange && racoon.playerStats.coins>=5)//No se ejecuta el if por contradiccion con la clase EnemyAIWave
                {
                    Debug.Log("Se ejecuta el if");
                    racoon.playerStats.coins -= Random.Range(1, 6);
                    Debug.Log("You've been robbed!");
                    StartCoroutine(Stealing(3f));
                    racoon.shootingRange = startingShootingRange;
                }
                Debug.Log("Se salta el if");
                timer = 0;
            }
            else
            {
                timer = 0;
            }
            //Quizá, haga falta añadir aquí un actionExecuted = false;
        }
    }

    private IEnumerator Stealing(float seconds)
    {
        racoon.detectionRange = 0f;
        racoon.agent.SetDestination(originalPosition);
        //Añadir aquí una línea de código que haga más smooth el giro del enemigo a su posicion original
        yield return new WaitForSeconds(seconds); //Este valor deberá ser cambiado por una variable que calcule el tiempo que tardaría el mapache en volver a su posición original 
        racoon.detectionRange = startingDetectionRange;
        racoon.canShoot = true;
        actionExecuted = false;
        racoon.isStealing = false;
    }
}
