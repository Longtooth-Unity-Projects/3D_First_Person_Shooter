using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerMaxHealth = 100;
    [SerializeField] private int playerCurrentHealth;   //TODO debuggin and testing
    [SerializeField] TextMeshProUGUI healthText;

    //cached references
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        playerCurrentHealth = playerMaxHealth;
        UpdateHealthDisplay();
    }

  
    public void ReduceHealth(int amountToReduce)
    {
        playerCurrentHealth -= amountToReduce;
        UpdateHealthDisplay();

        if (playerCurrentHealth <= 0)
            ProcessDeath();
    }


    private void UpdateHealthDisplay()
    {
        healthText.text = $"Health: {playerCurrentHealth}";
    }


    private void ProcessDeath()
    {
        gameManager.HandlePlayerDeath();
    }
}
