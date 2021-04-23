using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO combine with Enemy
public class Enemy : MonoBehaviour
{
    public static Action DamageTaken;
    public static Action Died;

    [SerializeField] int maxHealthPoints = 100;
    [SerializeField] int damage = 10;

    private int currentHealthPoints;

    public bool isAlive { get; private set; }

    //cached references
    private Player target;

    private void Start()
    {
        target = FindObjectOfType<Player>();

        currentHealthPoints = maxHealthPoints;
        isAlive = true;
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
        isAlive = false;
        Died?.Invoke();
    }

    //TODO called from animation, decouple this
    private void EnemyAttackHitEvent()
    {
        if (target == null) return;

        target.ReduceHealth(damage);
    }

}
