using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{



    //cached references
    [SerializeField] private Canvas gameOverCanvas;
    private AudioSource[] audioSources;


    private void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    
    private void Start()
    {
        audioSources = FindObjectsOfType<AudioSource>(true);
        gameOverCanvas.enabled = false;
    }


    public void HandlePlayerDeath()
    {
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StopAllAudio();
    }


    private void StopAllAudio()
    {
        foreach (AudioSource source in audioSources)
        {
            if (source.enabled == true)
            {
                source.Stop();
            }
        }
    }
}
