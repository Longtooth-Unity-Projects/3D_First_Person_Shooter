using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerMaxHealth = 100;
    [SerializeField] private int playerCurrentHealth;   //TODO debuggin and testing

    //cached references
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

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
        gameManager.HandlePlayerDeath();
    }
}
