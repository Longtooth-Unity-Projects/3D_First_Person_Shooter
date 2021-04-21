using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int playerMaxHealth = 100;
    [SerializeField] private int playerCurrentHealth;   //TODO debuggin and testing


    private void Start()
    {
        playerCurrentHealth = playerMaxHealth;
    }

  
    public void ReduceHealth(int amountToReduce)
    {
        playerCurrentHealth -= amountToReduce;

        if (playerCurrentHealth <= 0)
            ProcessDeath();
    }


    private void ProcessDeath()
    {
        Debug.Log("I'm dead");
    }
}
