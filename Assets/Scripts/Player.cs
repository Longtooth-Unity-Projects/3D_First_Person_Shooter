using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerMaxHealth = 100;
    [SerializeField] private int playerCurrentHealth;   //TODO debuggin and testing
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private AudioClip soundRunningGrass;

    //cached references
    GameManager gameManager;
    RigidbodyFirstPersonController rigidbodyFPC;
    AudioSource audioSource;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rigidbodyFPC = GetComponent<RigidbodyFirstPersonController>();
        audioSource = GetComponent<AudioSource>();

        playerCurrentHealth = playerMaxHealth;
        UpdateHealthDisplay();
    }

    private void Update()
    {
        ProcessMovementSound();       
    }

    private void ProcessMovementSound()
    {
        if ((rigidbodyFPC.MovingForward || rigidbodyFPC.MovingBackward || rigidbodyFPC.Strafing) && rigidbodyFPC.Grounded)
            StartNewAudioLoop(audioSource, soundRunningGrass);
        else
            StartNewAudioLoop(audioSource, null);
    }

    private void StartNewAudioLoop(AudioSource audioSource, AudioClip clipToStart)
    {
        //dont want to reinitiate since it is a loop
        if (audioSource.isPlaying.Equals(clipToStart))
            return;

        if (clipToStart == null)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.Stop();
            audioSource.loop = true;
            audioSource.clip = clipToStart;
            audioSource.Play();
        }
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
