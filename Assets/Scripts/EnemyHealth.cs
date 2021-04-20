using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealthPoints = 10;

    private int currentHealthPoints;

    private void Start()
    {
        currentHealthPoints = maxHealthPoints;
    }


    public void ReduceHealth(int amountToReduce)
    {
        currentHealthPoints -= amountToReduce;

        if (currentHealthPoints <= 0)
            ProcessDeath();
    }


    private void ProcessDeath()
    {
        Destroy(gameObject);
    }

}
