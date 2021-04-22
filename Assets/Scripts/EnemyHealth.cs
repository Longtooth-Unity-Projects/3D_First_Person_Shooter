using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO combine with EnemyAttack
public class EnemyHealth : MonoBehaviour
{
    public static Action DamageTaken;


    [SerializeField] int maxHealthPoints = 10;

    private int currentHealthPoints;

    private void Start()
    {
        currentHealthPoints = maxHealthPoints;
    }


    public void TakeDamage(int amountToReduce)
    {
        DamageTaken?.Invoke();


        currentHealthPoints -= amountToReduce;

        if (currentHealthPoints <= 0)
            ProcessDeath();
    }


    private void ProcessDeath()
    {
        Destroy(gameObject);
    }

}
