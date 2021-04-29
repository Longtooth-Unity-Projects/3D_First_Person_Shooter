using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{



    //cached references
    [SerializeField] private Canvas gameOverCanvas; //TODO remove this to a UI manager
    [SerializeField] private Canvas gameBeginCanvas; //TODO remove this to a UI manager
    private AudioSource[] allAudioSources;

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
        allAudioSources = FindObjectsOfType<AudioSource>(true);
        gameOverCanvas.enabled = false;
        gameBeginCanvas.enabled = false;

        //PauseAndLoadCanvas(gameBeginCanvas);
/*        gameBeginCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StopAllAudio(allAudioSources);*/

    }


    public void HandlePlayerDeath()
    {
        PauseAndLoadCanvas(gameOverCanvas);
    }

    private void PauseAndLoadCanvas(Canvas canvasToLoad)
    {
        canvasToLoad.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StopAllAudio(allAudioSources);
    }


    private void StopAllAudio(AudioSource[] audioSources)
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
